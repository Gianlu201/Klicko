using Klicko_be.DTOs.Account;
using Klicko_be.DTOs.Cart;
using Klicko_be.DTOs.CartExperience;
using Klicko_be.DTOs.Category;
using Klicko_be.DTOs.Experience;
using Klicko_be.Models;
using Klicko_be.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Klicko_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("GetCart/{cartId:guid}")]
        public async Task<IActionResult> GetCart(Guid cartId)
        {
            try
            {
                var cart = await _cartService.GetCartByIdAsync(cartId);

                if (cart == null)
                {
                    return BadRequest(
                        new GetCartByIdResponseDto()
                        {
                            Message = "Something went wrong!",
                            Cart = null,
                        }
                    );
                }

                var cartDto = new CartDto()
                {
                    CartId = cart.CartId,
                    UserId = cart.UserId,
                    CreatedAt = cart.CreatedAt,
                    UpdatedAt = cart.UpdatedAt,
                    User =
                        cart.User != null
                            ? new UserSimpleDto()
                            {
                                UserId = cart.User.Id,
                                FirstName = cart.User.FirstName,
                                LastName = cart.User.LastName,
                                Email = cart.User.Email,
                            }
                            : null,
                    CartExperiences =
                        (cart.CartExperiences != null && cart.CartExperiences.Count > 0)
                            ? cart
                                .CartExperiences.Select(expCart =>
                                    expCart.Experience != null
                                        ? new ExperienceForCartDto()
                                        {
                                            ExperienceId = expCart.Experience.ExperienceId,
                                            Title = expCart.Experience.Title,
                                            CategoryId = expCart.Experience.CategoryId,
                                            Place = expCart.Experience.Place,
                                            Price = expCart.Experience.Price,
                                            Quantity = expCart.Quantity,
                                            IsFreeCancellable = expCart
                                                .Experience
                                                .IsFreeCancellable,
                                            Sale = expCart.Experience.Sale,
                                            CoverImage = expCart.Experience.CoverImage,
                                            Category =
                                                expCart.Experience.Category != null
                                                    ? new CategorySimpleDto()
                                                    {
                                                        CategoryId = expCart
                                                            .Experience
                                                            .Category
                                                            .CategoryId,
                                                        Name = expCart.Experience.Category.Name,
                                                        Description = expCart
                                                            .Experience
                                                            .Category
                                                            .Description,
                                                        Image = expCart.Experience.Category.Image,
                                                        Icon = expCart.Experience.Category.Icon,
                                                    }
                                                    : null,
                                        }
                                        : null
                                )
                                .ToList()
                            : null,
                };

                return Ok(
                    new GetCartByIdResponseDto()
                    {
                        Message = "Cart retrieved successfully!",
                        Cart = cartDto,
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("AddExperience/{cartId:guid}")]
        public async Task<IActionResult> AddExperience(
            [FromBody] CreateCartExperienceRequestDto createCartExperience,
            Guid cartId
        )
        {
            try
            {
                var newCartExperience = new CartExperience()
                {
                    CartExperienceId = Guid.NewGuid(),
                    ExperienceId = createCartExperience.ExperienceId,
                    CartId = cartId,
                    CreatedAt = DateTime.UtcNow,
                };

                var result = await _cartService.AddExperienceToCartAsync(newCartExperience, cartId);

                return result
                    ? Ok(new EditCartResponseDto() { Message = "Cart modified successfully!" })
                    : BadRequest(new EditCartResponseDto() { Message = "Something went wrong!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("AddExperienceUnit/{cartId:guid}")]
        public async Task<IActionResult> AddExperienceUnit(
            [FromBody] Guid experienceId,
            Guid cartId
        )
        {
            try
            {
                var result = await _cartService.AddExperienceUnitToCartAsync(cartId, experienceId);

                return result
                    ? Ok(new EditCartResponseDto() { Message = "Cart modified successfully!" })
                    : BadRequest(new EditCartResponseDto() { Message = "Something went wrong!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("RemoveExperience/{cartId:guid}")]
        public async Task<IActionResult> RemoveExperience([FromBody] Guid experienceId, Guid cartId)
        {
            try
            {
                var result = await _cartService.RemoveExperienceFromCartAsync(experienceId, cartId);

                return result
                    ? Ok(new EditCartResponseDto() { Message = "Cart modified successfully!" })
                    : BadRequest(new EditCartResponseDto() { Message = "Something went wrong!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("RemoveExperienceUnit/{cartId:guid}")]
        public async Task<IActionResult> RemoveExperienceUnit(
            [FromBody] Guid experienceId,
            Guid cartId
        )
        {
            try
            {
                var result = await _cartService.RemoveExperienceUnitToCartAsync(
                    cartId,
                    experienceId
                );

                return result
                    ? Ok(new EditCartResponseDto() { Message = "Cart modified successfully!" })
                    : BadRequest(new EditCartResponseDto() { Message = "Something went wrong!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("RemoveAllExperience/{cartId:guid}")]
        public async Task<IActionResult> RemoveAllExperience(Guid cartId)
        {
            try
            {
                var result = await _cartService.RemoveAllExperienceFromCartAsync(cartId);

                return result
                    ? Ok(new EditCartResponseDto() { Message = "Cart modified successfully!" })
                    : BadRequest(new EditCartResponseDto() { Message = "Something went wrong!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
