using Klicko_be.Data;
using Klicko_be.DTOs.Cart;
using Klicko_be.Models;
using Microsoft.EntityFrameworkCore;

namespace Klicko_be.Services
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
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

        public async Task<Cart?> GetCartByIdAsync(Guid cartId)
        {
            try
            {
                var cart = await _context
                    .Carts.Include(c => c.CartExperiences)!
                    .ThenInclude(ce => ce.Experience)
                    .FirstOrDefaultAsync(c => c.CartId == cartId);

                return cart;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateCartDateByIdAsync(Guid cartId)
        {
            try
            {
                var cart = await GetCartByIdAsync(cartId);

                if (cart == null)
                {
                    return false;
                }

                cart.UpdatedAt = DateTime.Now;

                return await TrySaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddExperienceToCartAsync(
            CartExperience newCartExperience,
            Guid cartId
        )
        {
            try
            {
                var cart = await GetCartByIdAsync(cartId);

                if (cart == null)
                {
                    return false;
                }

                var cartExperience = await _context.CartExperiences.FirstOrDefaultAsync(ce =>
                    ce.CartId == cartId && ce.ExperienceId == newCartExperience.ExperienceId
                );

                if (cartExperience == null)
                {
                    _context.CartExperiences.Add(newCartExperience);
                }
                else
                {
                    cartExperience.Quantity++;
                }

                var result = await TrySaveAsync();

                if (result)
                {
                    await UpdateCartDateByIdAsync(cartId);
                }

                return result;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddExperienceUnitToCartAsync(Guid cartId, Guid experienceId)
        {
            try
            {
                var cartExperience = await _context.CartExperiences.FirstOrDefaultAsync(ce =>
                    ce.CartId == cartId && ce.ExperienceId == experienceId
                );

                if (cartExperience == null)
                {
                    return false;
                }

                cartExperience.Quantity++;

                var result = await TrySaveAsync();

                if (result)
                {
                    await UpdateCartDateByIdAsync(cartId);
                }

                return result;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveExperienceFromCartAsync(Guid experienceId, Guid cartId)
        {
            try
            {
                var cartExperience = await _context.CartExperiences.FirstOrDefaultAsync(ce =>
                    ce.ExperienceId == experienceId && ce.CartId == cartId
                );

                if (cartExperience == null || cartExperience.CartId != cartId)
                {
                    return false;
                }

                _context.CartExperiences.Remove(cartExperience);

                var result = await TrySaveAsync();

                if (result)
                {
                    await UpdateCartDateByIdAsync(cartId);
                }

                return result;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveExperienceUnitToCartAsync(Guid cartId, Guid experienceId)
        {
            try
            {
                var cartExperience = await _context.CartExperiences.FirstOrDefaultAsync(ce =>
                    ce.CartId == cartId && ce.ExperienceId == experienceId
                );

                if (cartExperience == null)
                {
                    return false;
                }

                if (cartExperience.Quantity == 1)
                {
                    _context.CartExperiences.Remove(cartExperience);
                }
                else
                {
                    cartExperience.Quantity--;
                }

                var result = await TrySaveAsync();

                if (result)
                {
                    await UpdateCartDateByIdAsync(cartId);
                }

                return result;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveAllExperienceFromCartAsync(Guid cartId)
        {
            try
            {
                var cart = await GetCartByIdAsync(cartId);

                if (cart == null)
                {
                    return false;
                }

                var cartExperiences = await _context
                    .CartExperiences.Where(ce => ce.CartId == cartId)
                    .ToListAsync();

                if (cartExperiences == null || cartExperiences.Count == 0)
                {
                    return false;
                }
                _context.CartExperiences.RemoveRange(cartExperiences);
                var result = await TrySaveAsync();

                if (result)
                {
                    await UpdateCartDateByIdAsync(cartId);
                }

                return result;
            }
            catch
            {
                return false;
            }
        }
    }
}
