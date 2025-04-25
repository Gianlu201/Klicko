using Klicko_be.Data;
using Klicko_be.Models;
using Microsoft.EntityFrameworkCore;

namespace Klicko_be.Services
{
    public class FidelityCardService
    {
        private readonly ApplicationDbContext _context;
        private readonly CouponService _couponService;

        public FidelityCardService(ApplicationDbContext context, CouponService couponService)
        {
            _context = context;
            _couponService = couponService;
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

        public async Task<FidelityCard?> GetFidelityCardByUserIdAsync(string userId)
        {
            try
            {
                return await _context
                    .FidelityCards.Include(f => f.User)
                    .FirstOrDefaultAsync(f => f.UserId == userId);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> ConvertPointsInCouponAsync(int points, Guid cardId)
        {
            try
            {
                var fidelityCard = await GetFidelityCardByIdAsync(cardId);

                if (fidelityCard == null)
                {
                    return false;
                }

                if (fidelityCard.AvailablePoints < points)
                {
                    return false;
                }

                var pointsToConvert = 0;
                var fixedSaleAmount = 0;

                switch (points)
                {
                    case 500:
                        pointsToConvert = 500;
                        fixedSaleAmount = 5;
                        break;

                    case 950:
                        pointsToConvert = 950;
                        fixedSaleAmount = 10;
                        break;

                    case 1800:
                        pointsToConvert = 1800;
                        fixedSaleAmount = 20;
                        break;

                    default:
                        return false;
                }

                var randomCode = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

                var coupon = new Coupon()
                {
                    CouponId = Guid.NewGuid(),
                    PercentualSaleAmount = 0,
                    FixedSaleAmount = fixedSaleAmount,
                    IsActive = true,
                    IsUniversal = false,
                    ExpireDate = DateTime.UtcNow.AddDays(7),
                    MinimumAmount = fixedSaleAmount * 5,
                    UserId = fidelityCard.UserId,
                    Code = randomCode,
                };

                var result = await _couponService.CreateCouponAsync(coupon);

                if (!result)
                {
                    return false;
                }

                fidelityCard.AvailablePoints -= pointsToConvert;

                return await TrySaveAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}
