using System.Security.Claims;
using Klicko_be.DTOs.Coupon;
using Klicko_be.Models;
using Klicko_be.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Klicko_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly CouponService _couponService;

        public CouponController(CouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet("getAllUserCoupons")]
        [Authorize(Roles = "Admin, Seller, User")]
        public async Task<IActionResult> GetAllCoupons()
        {
            try
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var userId = user!.Value;

                var coupons = await _couponService.GetAllCouponsAsync(userId);

                if (coupons == null || coupons.Count == 0)
                {
                    return NotFound(
                        new GetAllCouponsResponseDto()
                        {
                            Message = "No coupons found.",
                            AvailableCoupons = null,
                            UnavailableCoupons = null,
                        }
                    );
                }

                var availableCoupons = coupons
                    .Where(c => c.IsActive)
                    .Select(c => new CouponDto()
                    {
                        CouponId = c.CouponId,
                        PercentualSaleAmount = c.PercentualSaleAmount,
                        FixedSaleAmount = c.FixedSaleAmount,
                        IsActive = c.IsActive,
                        IsUniversal = c.IsUniversal,
                        ExpireDate = c.ExpireDate,
                        Code = c.Code,
                        MinimumAmount = c.MinimumAmount,
                        UserId = c.UserId,
                    })
                    .ToList();

                var unavailableCoupons = coupons
                    .Where(c => !c.IsActive)
                    .Select(c => new CouponDto()
                    {
                        CouponId = c.CouponId,
                        PercentualSaleAmount = c.PercentualSaleAmount,
                        FixedSaleAmount = c.FixedSaleAmount,
                        IsActive = c.IsActive,
                        IsUniversal = c.IsUniversal,
                        ExpireDate = c.ExpireDate,
                        Code = c.Code,
                        MinimumAmount = c.MinimumAmount,
                        UserId = c.UserId,
                    })
                    .ToList();

                return Ok(
                    new GetAllCouponsResponseDto()
                    {
                        Message = $"{coupons.Count} found!",
                        AvailableCoupons = availableCoupons,
                        UnavailableCoupons = unavailableCoupons,
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Internal server error: {ex.Message}"
                );
            }
        }

        [HttpGet("getCouponByCode/{couponCode}")]
        [Authorize(Roles = "Admin, Seller, User")]
        public async Task<IActionResult> GetAllCoupons(string couponCode)
        {
            try
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var userId = user!.Value;

                var coupon = await _couponService.GetCouponByCodeAsync(userId, couponCode);

                if (coupon == null)
                {
                    return NotFound(
                        new GetCouponResponseDto() { Message = "No coupons found.", Coupon = null }
                    );
                }

                var couponDto = new CouponDto()
                {
                    CouponId = coupon.CouponId,
                    PercentualSaleAmount = coupon.PercentualSaleAmount,
                    FixedSaleAmount = coupon.FixedSaleAmount,
                    IsActive = coupon.IsActive,
                    IsUniversal = coupon.IsUniversal,
                    ExpireDate = coupon.ExpireDate,
                    Code = coupon.Code,
                    MinimumAmount = coupon.MinimumAmount,
                    UserId = coupon.UserId,
                };

                return Ok(
                    new GetCouponResponseDto() { Message = "Coupon found!", Coupon = couponDto }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Internal server error: {ex.Message}"
                );
            }
        }

        [HttpPost("createCoupon")]
        [Authorize(Roles = "Admin, Seller, User")]
        public async Task<IActionResult> CreateCoupon(CreateCouponRequestDto createCoupon)
        {
            try
            {
                var newCoupon = new Coupon()
                {
                    CouponId = Guid.NewGuid(),
                    PercentualSaleAmount = createCoupon.PercentualSaleAmount,
                    FixedSaleAmount = createCoupon.FixedSaleAmount,
                    IsActive = createCoupon.IsActive,
                    IsUniversal = createCoupon.IsUniversal,
                    ExpireDate = createCoupon.ExpireDate,
                    MinimumAmount = createCoupon.MinimumAmount,
                    UserId = createCoupon.UserId,
                };

                var result = await _couponService.CreateCouponAsync(newCoupon);

                return result
                    ? Ok(new CreateCouponResponseDto() { Message = "Coupon created successfully!" })
                    : BadRequest(
                        new CreateCouponResponseDto() { Message = "Something went wrong!" }
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Internal server error: {ex.Message}"
                );
            }
        }

        [HttpPut("updateCoupon/{couponId:guid}")]
        [Authorize(Roles = "Admin, Seller, User")]
        public async Task<IActionResult> UpdateCouponValidityById(Guid couponId, bool isActive)
        {
            try
            {
                var result = await _couponService.UpdateCouponValidityByIdAsync(couponId, isActive);

                return result
                    ? Ok(new EditCouponResponseDto() { Message = "Coupon updated successfully!" })
                    : BadRequest(new EditCouponResponseDto() { Message = "Something went wrong!" });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Internal server error: {ex.Message}"
                );
            }
        }
    }
}
