using Klicko_be.Data;
using Klicko_be.Models;
using Microsoft.EntityFrameworkCore;

namespace Klicko_be.Services
{
    public class FidelityCardService
    {
        private readonly ApplicationDbContext _context;

        public FidelityCardService(ApplicationDbContext context)
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

        public async Task<FidelityCard?> GetFidelityCardByIdAsync(Guid cardId)
        {
            try
            {
                return await _context
                    .FidelityCards.Include(f => f.User)
                    .FirstOrDefaultAsync(f => f.FidelityCardId == cardId);
            }
            catch
            {
                return null;
            }
        }
    }
}
