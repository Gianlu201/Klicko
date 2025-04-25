using Klicko_be.DTOs.Account;
using Klicko_be.DTOs.FidelityCard;
using Klicko_be.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Klicko_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FidelityCardController : ControllerBase
    {
        private readonly FidelityCardService _fidelityCardService;

        public FidelityCardController(FidelityCardService fidelityCardService)
        {
            _fidelityCardService = fidelityCardService;
        }

        [HttpGet("getFidelityCardById/{cardId:guid}")]
        public async Task<IActionResult> GetFidelityCardById(Guid cardId)
        {
            try
            {
                var fidelityCard = await _fidelityCardService.GetFidelityCardByIdAsync(cardId);

                if (fidelityCard == null)
                {
                    return BadRequest(
                        new GetFidelityCardResponse()
                        {
                            Message = "Something went wrong!",
                            FidelityCard = null,
                        }
                    );
                }

                var fidelityCardDto = new FidelityCardDto()
                {
                    FidelityCardId = fidelityCard.FidelityCardId,
                    CardNumber = fidelityCard.CardNumber,
                    Points = fidelityCard.Points,
                    AvailablePoints = fidelityCard.AvailablePoints,
                    UserId = fidelityCard.UserId,
                    User =
                        fidelityCard.User != null
                            ? new UserSimpleDto()
                            {
                                UserId = fidelityCard.User.Id,
                                FirstName = fidelityCard.User.FirstName,
                                LastName = fidelityCard.User.LastName,
                                Email = fidelityCard.User.Email,
                            }
                            : null,
                };

                return Ok(
                    new GetFidelityCardResponse()
                    {
                        Message = "Fidelity card find successfully!",
                        FidelityCard = fidelityCardDto,
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

        [HttpPut("convertPointsInCoupon/{cardId:guid}")]
        [Authorize(Roles = "Admin, Seller, User")]
        public async Task<IActionResult> ConvertPointsInCoupon(
            [FromBody] ConvertPointsRequestDto requestDto,
            Guid cardId
        )
        {
            try
            {
                var result = await _fidelityCardService.ConvertPointsInCouponAsync(
                    requestDto.Points,
                    cardId
                );

                return result
                    ? Ok(
                        new ConvertPointsResponseDto()
                        {
                            Message = "Points converted successfully!",
                        }
                    )
                    : BadRequest(
                        new ConvertPointsResponseDto() { Message = "Something went wrong!" }
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
