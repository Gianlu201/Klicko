using Klicko_be.Data;
using Klicko_be.DTOs.Experience;
using Klicko_be.Models;
using Microsoft.EntityFrameworkCore;

namespace Klicko_be.Services
{
    public class ExperienceService
    {
        private readonly ApplicationDbContext _context;

        public ExperienceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> TrySaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Experience>?> GetAllExperienceAsAdminAsync()
        {
            try
            {
                var experiences = await _context
                    .Experiences.Include(e => e.Category)
                    .Include(e => e.Images)
                    .Include(e => e.CarryWiths)
                    .Include(e => e.UserCreator)
                    .Include(e => e.UserLastModify)
                    .OrderBy(e => e.IsDeleted)
                    .ToListAsync();

                return experiences;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Experience>?> GetAllExperienceAsync()
        {
            try
            {
                var experiences = await _context
                    .Experiences.Include(e => e.Category)
                    .Include(e => e.Images)
                    .Include(e => e.CarryWiths)
                    .OrderBy(e => e.IsDeleted)
                    .Where(e => e.IsDeleted == false)
                    .ToListAsync();

                return experiences;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Experience>?> GetAllHighlightedExperienceAsync()
        {
            try
            {
                var experiences = await _context
                    .Experiences.Include(e => e.Category)
                    .Where(e => e.IsInEvidence)
                    .AsNoTracking()
                    .ToListAsync();

                return experiences;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Experience>?> GetAllPopularExperienceAsync()
        {
            try
            {
                var experiences = await _context
                    .Experiences.Include(e => e.Category)
                    .Where(e => e.IsPopular)
                    .AsNoTracking()
                    .ToListAsync();

                return experiences;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Experience?> GetExperienceByIdAsync(Guid experienceId)
        {
            try
            {
                var experience = await _context
                    .Experiences.Include(e => e.Category)
                    .Include(e => e.Images)
                    .Include(e => e.CarryWiths)
                    .Include(e => e.UserCreator)
                    .Include(e => e.UserLastModify)
                    .FirstOrDefaultAsync(e => e.ExperienceId == experienceId);

                // l'esperienza include tutte le immagini associate all'esperienza tranne le immagini che hanno la proprietà IsCover = true
                if (experience != null)
                {
                    experience.Images = experience.Images.Where(i => i.IsCover == false).ToList();
                }

                return experience;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> EditExperienceByIdAsync(
            Guid experienceId,
            EditExperienceRequestDto experienceEdit,
            string userId
        )
        {
            try
            {
                var experience = await GetExperienceByIdAsync(experienceId);

                if (experience == null)
                {
                    return false;
                }

                experience.Title = experienceEdit.Title;
                experience.CategoryId = experienceEdit.CategoryId;
                experience.Duration = experienceEdit.Duration;
                experience.Place = experienceEdit.Place;
                experience.Price = experienceEdit.Price;
                experience.DescriptionShort = experienceEdit.DescriptionShort;
                experience.Description = experienceEdit.Description;
                experience.MaxParticipants = experienceEdit.MaxParticipants;
                experience.Organiser = experienceEdit.Organiser;
                experience.LastEditDate = DateTime.Now;
                experience.UserLastModifyId = userId;
                experience.IsFreeCancellable = experienceEdit.IsFreeCancellable;
                experience.IncludedDescription = experienceEdit.IncludedDescription;
                experience.Sale = experienceEdit.Sale;
                experience.IsInEvidence = experienceEdit.IsInEvidence;
                experience.IsPopular = experienceEdit.IsPopular;
                experience.ValidityInMonths = experienceEdit.ValidityInMonths;

                Image? prevCoverImg = null;
                if (experienceEdit.RemovedCoverImage)
                {
                    prevCoverImg = await _context.Images.FirstOrDefaultAsync(i =>
                        i.Url == experience.CoverImage && i.ExperienceId == experienceId
                    );

                    if (prevCoverImg == null)
                    {
                        return false;
                    }

                    var fileName =
                        Guid.NewGuid().ToString()
                        + Path.GetExtension(experienceEdit.CoverImage.FileName);
                    var uploadsPath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "uploads"
                    );

                    if (!Directory.Exists(uploadsPath))
                        Directory.CreateDirectory(uploadsPath);

                    var filePath = Path.Combine(uploadsPath, fileName);

                    await using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await experienceEdit.CoverImage.CopyToAsync(stream);
                    }

                    experience.CoverImage = fileName;

                    _context.Images.Add(
                        new Image
                        {
                            ImageId = Guid.NewGuid(),
                            Url = fileName,
                            ExperienceId = experience.ExperienceId,
                            IsCover = true,
                        }
                    );
                }

                if (experienceEdit.Images != null)
                {
                    var imagesList = new List<Image>();

                    foreach (var img in experienceEdit.Images)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(img.FileName);
                        var filePath = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot",
                            "uploads",
                            fileName
                        );

                        await using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await img.CopyToAsync(stream);
                        }

                        imagesList.Add(
                            new Image
                            {
                                ImageId = Guid.NewGuid(),
                                Url = fileName,
                                ExperienceId = experience.ExperienceId,
                                IsCover = false,
                            }
                        );
                    }

                    foreach (var img in imagesList)
                    {
                        _context.Images.Add(img);
                    }
                }

                if (experienceEdit.RemovedImages != null)
                {
                    // rimuovere i file fisici
                    foreach (var imgId in experienceEdit.RemovedImages)
                    {
                        var imgToRemove = await _context.Images.FindAsync(imgId);
                        if (imgToRemove != null)
                        {
                            var filePath = Path.Combine(
                                Directory.GetCurrentDirectory(),
                                "wwwroot",
                                "uploads",
                                imgToRemove.Url
                            );
                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }
                        }
                    }

                    var result2 = await TrySaveAsync();

                    if (!result2)
                    {
                        return false;
                    }

                    foreach (var imgId in experienceEdit.RemovedImages)
                    {
                        var imgToRemove = await _context.Images.FindAsync(imgId);
                        if (imgToRemove != null)
                        {
                            _context.Images.Remove(imgToRemove);
                        }
                    }

                    var result3 = await TrySaveAsync();

                    if (!result3)
                    {
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(experienceEdit.CarryWiths))
                {
                    // rimuovo tutti i carryWiths associati
                    var carryWiths = await _context
                        .CarryWiths.Where(c => c.ExperienceId == experienceId)
                        .ToListAsync();

                    if (carryWiths != null)
                    {
                        foreach (var carryWith in carryWiths)
                        {
                            _context.CarryWiths.Remove(carryWith);
                        }
                    }

                    // aggiungo i nuovi carryWiths
                    var carryWithsToAdd = experienceEdit
                        .CarryWiths.Split(',')
                        .Select(c => new CarryWith
                        {
                            CarryWithId = Guid.NewGuid(),
                            ExperienceId = experienceId,
                            Name = c.Trim(),
                        })
                        .ToList();
                    _context.CarryWiths.AddRange(carryWithsToAdd);
                }

                var result = await TrySaveAsync();
                if (result)
                {
                    if (prevCoverImg != null)
                    {
                        _context.Images.Remove(prevCoverImg);

                        // rimuovere anche il file fisico
                        var filePath = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot",
                            "uploads",
                            prevCoverImg.Url
                        );
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                        return await TrySaveAsync();
                    }
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateExperienceAsync(Experience newExperience)
        {
            try
            {
                _context.Experiences.Add(newExperience);

                if (newExperience.CoverImage != null)
                {
                    _context.Images.Add(
                        new Image
                        {
                            ImageId = Guid.NewGuid(),
                            Url = newExperience.CoverImage,
                            ExperienceId = newExperience.ExperienceId,
                            IsCover = true,
                        }
                    );
                }

                return await TrySaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SoftDeleteExperienceByIdAsync(Guid experienceId)
        {
            try
            {
                var experience = await GetExperienceByIdAsync(experienceId);

                if (experience == null)
                {
                    return false;
                }

                experience.IsDeleted = true;

                return await TrySaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RestoreExperienceByIdAsync(Guid experienceId)
        {
            try
            {
                var experience = await GetExperienceByIdAsync(experienceId);

                if (experience == null)
                {
                    return false;
                }

                experience.IsDeleted = false;

                return await TrySaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteExperienceByIdAsync(Guid experienceId)
        {
            try
            {
                var experience = await GetExperienceByIdAsync(experienceId);

                if (experience == null)
                {
                    return false;
                }

                _context.Experiences.Remove(experience);

                return await TrySaveAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}
