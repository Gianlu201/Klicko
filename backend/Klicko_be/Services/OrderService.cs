using Klicko_be.Data;
using Klicko_be.Models;
using Microsoft.EntityFrameworkCore;

namespace Klicko_be.Services
{
    public class OrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<bool> TrySaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Order>?> GetAllOrdersAsync()
        {
            try
            {
                var orders = await _context
                    .Orders.Include(o => o.OrderExperiences)
                    .ThenInclude(oe => oe.Experience)
                    .ThenInclude(e => e.Category)
                    .Include(o => o.User)
                    .ToListAsync();

                return orders;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateOrderAsync(Order newOrder)
        {
            try
            {
                _context.Orders.Add(newOrder);

                return await TrySaveAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}
