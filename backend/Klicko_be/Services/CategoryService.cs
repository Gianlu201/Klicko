using Klicko_be.Data;
using Klicko_be.Models;
using Microsoft.EntityFrameworkCore;

namespace Klicko_be.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<bool> TrySaveAsync()
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

        public async Task<List<Category>?> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();

                return categories;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateCategoryAsync(Category newCategory)
        {
            try
            {
                _context.Categories.Add(newCategory);

                return await TrySaveAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}
