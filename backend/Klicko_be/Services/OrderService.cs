using Klicko_be.Data;
using Klicko_be.Models;
using Microsoft.EntityFrameworkCore;

namespace Klicko_be.Services
{
    public class OrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly CartService _cartService;

        public OrderService(ApplicationDbContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public async Task<bool> TrySaveAsync()
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
                    .Include(o => o.User)
                    .Include(o => o.Vouchers)
                    .ToListAsync();

                return orders;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Order>?> GetAllOrdersForUserByIdAsync(string userId)
        {
            try
            {
                var orders = await _context
                    .Orders.Include(o => o.OrderExperiences)
                    .Include(o => o.User)
                    .Include(o => o.Vouchers)
                    .Where(o => o.UserId == userId)
                    .ToListAsync();

                return orders;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            try
            {
                var order = await _context
                    .Orders.Include(o => o.OrderExperiences)
                    .Include(o => o.User)
                    .Include(o => o.Vouchers)
                    .ThenInclude(v => v.Category)
                    .FirstOrDefaultAsync(o => o.OrderId == orderId);

                return order;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateOrderAsync(Order newOrder, string userId)
        {
            try
            {
                _context.Orders.Add(newOrder);

                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId);

                var result = await _cartService.RemoveAllExperienceFromCartAsync(user!.CartId);

                return result;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditOrderStateAsync(Guid orderId, string orderState)
        {
            try
            {
                var order = await _context.Orders.FindAsync(orderId);

                if (order == null)
                {
                    return false;
                }

                order.State = orderState;

                return await TrySaveAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}
