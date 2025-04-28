using Klicko_be.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Klicko_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost("sendNewsLetter")]
        public async Task<IActionResult> SendNewsLetter([FromBody] string email)
        {
            try
            {
                var result = await EmailService.SendNewsLetterAsync(email);
                return Ok("Newsletter sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
