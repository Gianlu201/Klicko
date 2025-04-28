using System.Security.Claims;
using Klicko_be.DTOs.Account;
using Klicko_be.DTOs.Category;
using Klicko_be.DTOs.Voucher;
using Klicko_be.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Klicko_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly VoucherService _voucherService;

        public VoucherController(VoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        [HttpGet("getAllUserVouchers")]
        [Authorize(Roles = "Admin, Seller, User")]
        public async Task<IActionResult> GetAllUserVouchers()
        {
            try
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var userId = user!.Value;

                var vouchers = await _voucherService.GetAllUserVouchersAsync(userId);
                if (vouchers == null)
                {
                    return NotFound(
                        new GetVouchersListResponseDto()
                        {
                            Message = "No vouchers found.",
                            Vouchers = null,
                        }
                    );
                }

                var vouchersList = vouchers
                    .Select(v => new VoucherDto()
                    {
                        VoucherId = v.VoucherId,
                        Title = v.Title,
                        CategoryId = v.CategoryId,
                        Duration = v.Duration,
                        Place = v.Place,
                        Price = v.Price,
                        Organiser = v.Organiser,
                        IsFreeCancellable = v.IsFreeCancellable,
                        VoucherCode = v.VoucherCode,
                        ReservationDate = v.ReservationDate,
                        IsUsed = v.IsUsed,
                        ExpirationDate = v.ExpirationDate,
                        UserId = v.UserId,
                        CreatedAt = v.CreatedAt,
                        Category = new CategorySimpleDto()
                        {
                            CategoryId = v.Category!.CategoryId,
                            Name = v.Category.Name,
                            Description = v.Category.Description,
                        },
                        User =
                            v.User != null
                                ? new UserSimpleDto()
                                {
                                    UserId = v.User.Id,
                                    FirstName = v.User.FirstName,
                                    LastName = v.User.LastName,
                                    Email = v.User.Email,
                                }
                                : null,
                    })
                    .ToList();

                return Ok(
                    new GetVouchersListResponseDto()
                    {
                        Message = $"{vouchersList.Count} vouchers found.",
                        Vouchers = vouchersList,
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message }
                );
            }
        }

        [HttpGet("getVoucherByCode/{voucherCode}")]
        [Authorize(Roles = "Admin, Seller, User")]
        public async Task<IActionResult> GetVoucherByCode(string voucherCode)
        {
            try
            {
                var voucher = await _voucherService.GetVoucherByCodeAsync(voucherCode);

                if (voucher == null)
                {
                    return BadRequest(
                        new GetVoucherResponseDto()
                        {
                            Message = "Voucher not found.",
                            Voucher = null,
                        }
                    );
                }

                var voucherDto = new VoucherDto()
                {
                    VoucherId = voucher.VoucherId,
                    Title = voucher.Title,
                    CategoryId = voucher.CategoryId,
                    Duration = voucher.Duration,
                    Place = voucher.Place,
                    Price = voucher.Price,
                    Organiser = voucher.Organiser,
                    IsFreeCancellable = voucher.IsFreeCancellable,
                    VoucherCode = voucher.VoucherCode,
                    ReservationDate = voucher.ReservationDate,
                    IsUsed = voucher.IsUsed,
                    ExpirationDate = voucher.ExpirationDate,
                    UserId = voucher.UserId,
                    CreatedAt = voucher.CreatedAt,
                    Category = new CategorySimpleDto()
                    {
                        CategoryId = voucher.Category!.CategoryId,
                        Name = voucher.Category.Name,
                        Description = voucher.Category.Description,
                    },
                    User =
                        voucher.User != null
                            ? new UserSimpleDto()
                            {
                                UserId = voucher.User.Id,
                                FirstName = voucher.User.FirstName,
                                LastName = voucher.User.LastName,
                                Email = voucher.User.Email,
                            }
                            : null,
                };

                return Ok(
                    new GetVoucherResponseDto() { Message = "Voucher found.", Voucher = voucherDto }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message }
                );
            }
        }

        [HttpPut("editVoucher/{voucherId:guid}")]
        [Authorize(Roles = "Admin, Seller, User")]
        public async Task<IActionResult> EditVoucher(
            [FromBody] EditVoucherRequestDto editVoucher,
            Guid voucherId
        )
        {
            try
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var userId = user!.Value;

                var result = await _voucherService.EditVoucherByIdAsync(
                    voucherId,
                    editVoucher,
                    userId
                );

                return result
                    ? Ok(new EditVoucherResponseDto() { Message = "Voucher updated successfully!" })
                    : BadRequest(
                        new EditVoucherResponseDto() { Message = "Something went wrong!" }
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message }
                );
            }
        }
    }
}
