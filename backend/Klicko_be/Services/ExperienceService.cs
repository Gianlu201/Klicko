using Klicko_be.Data;
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
