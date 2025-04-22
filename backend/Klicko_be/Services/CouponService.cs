using Klicko_be.Data;
using Klicko_be.Models;
using Microsoft.EntityFrameworkCore;

namespace Klicko_be.Services
{
    public class CouponService
    {
        private readonly ApplicationDbContext _context;

        public CouponService(ApplicationDbContext context)
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

        public async Task<List<Coupon>?> GetAllCouponsAsync(string userId)
        {
            try
            {
                return await _context.Coupons.Where(c => c.UserId == userId).ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateCouponAsync(Coupon newCoupon)
        {
            try
            {
                await _context.Coupons.AddAsync(newCoupon);
                return await TrySaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateCouponValidityByIdAsync(Guid couponId, bool isActive)
        {
            try
            {
                var coupon = await _context.Coupons.FindAsync(couponId);
                if (coupon == null)
                    return false;
                coupon.IsActive = isActive;
                return await TrySaveAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}
