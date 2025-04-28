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
                var couponsList = await _context
                    .Coupons.Where(c => c.UserId == userId)
                    .ToListAsync();

                if (couponsList.Count > 0)
                {
                    var couponEditedCounter = 0;

                    foreach (var coupon in couponsList)
                    {
                        var result = await CheckCouponAvailability(coupon);
                        if (result)
                        {
                            couponEditedCounter++;
                        }
                    }

                    if (couponEditedCounter > 0)
                    {
                        return await _context.Coupons.Where(c => c.UserId == userId).ToListAsync();
                    }
                }

                return couponsList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Coupon?> GetCouponByCodeAsync(string userId, string couponCode)
        {
            try
            {
                var coupon = await _context.Coupons.FirstOrDefaultAsync(c =>
                    c.Code == couponCode
                    && c.UserId == userId
                    && c.IsActive
                    && (c.ExpireDate > DateTime.UtcNow || c.ExpireDate == null)
                );

                if (coupon != null)
                {
                    var result = await CheckCouponAvailability(coupon);

                    if (result)
                    {
                        return await _context.Coupons.FirstOrDefaultAsync(c =>
                            c.Code == couponCode
                            && c.UserId == userId
                            && c.IsActive
                            && (c.ExpireDate > DateTime.UtcNow || c.ExpireDate == null)
                        );
                    }
                }

                return coupon;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Coupon?> GetCouponByIdAsync(Guid couponId)
        {
            try
            {
                var coupon = await _context.Coupons.FirstOrDefaultAsync(c =>
                    c.CouponId == couponId
                    && c.IsActive
                    && (c.ExpireDate > DateTime.UtcNow || c.ExpireDate == null)
                );

                if (coupon != null)
                {
                    var result = await CheckCouponAvailability(coupon);

                    if (result)
                    {
                        return await _context.Coupons.FirstOrDefaultAsync(c =>
                            c.CouponId == couponId
                            && c.IsActive
                            && (c.ExpireDate > DateTime.UtcNow || c.ExpireDate == null)
                        );
                    }
                }

                return coupon;
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

        public async Task<bool> CheckCouponAvailability(Coupon coupon)
        {
            try
            {
                if (coupon.IsActive && coupon.ExpireDate <= DateTime.Now)
                {
                    await UpdateCouponValidityByIdAsync(coupon.CouponId, false);
                    return true;
                }

                return false;
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
