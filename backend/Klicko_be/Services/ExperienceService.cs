﻿using Klicko_be.Data;
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

        public async Task<List<Experience>?> GetAllExperienceAsync()
        {
            try
            {
                var experiences = await _context
                    .Experiences.Include(e => e.Category)
                    .Include(e => e.Images)
                    .Include(e => e.CarryWiths)
                    .Include(e => e.UserCreator)
                    .Include(e => e.UserLastModify)
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

                return experience;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> EditEcperienceByIdAsync(
            Guid experienceId,
            EditExperienceRequestDto experienceEdit
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
                experience.LastEditDate = experienceEdit.LastEditDate;
                experience.UserLastModifyId = experienceEdit.UserLastModifyId;
                experience.IsFreeCancellable = experienceEdit.IsFreeCancellable;
                experience.IncludedDescription = experienceEdit.IncludedDescription;
                experience.Sale = experienceEdit.Sale;
                experience.IsInEvidence = experienceEdit.IsInEvidence;
                experience.IsPopular = experienceEdit.IsPopular;
                experience.CoverImage = experienceEdit.CoverImage;
                experience.ValidityInMonths = experienceEdit.ValidityInMonths;

                if (experienceEdit.Images != null)
                {
                    experience.Images = experienceEdit
                        .Images.Select(i => new Image()
                        {
                            Url = i.Url,
                            AltText = i.AltText,
                            ExperienceId = experience.ExperienceId,
                        })
                        .ToList();
                }

                if (experienceEdit.CarryWiths != null)
                {
                    experience.CarryWiths = experienceEdit
                        .CarryWiths.Select(c => new CarryWith()
                        {
                            Name = c.Name,
                            ExperienceId = experience.ExperienceId,
                        })
                        .ToList();
                }

                return await TrySaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateExperienceAsync(Experience newExoerience)
        {
            try
            {
                _context.Experiences.Add(newExoerience);

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
