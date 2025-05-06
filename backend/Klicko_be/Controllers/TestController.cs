using Klicko_be.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Klicko_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API online!");
        }

        [HttpGet("getConnectionString")]
        public IActionResult ConnectionString([FromServices] IConfiguration config)
        {
            return Ok(config.GetConnectionString("DefaultConnection") ?? "NULL");
        }

        [HttpGet("dbcheck")]
        public async Task<IActionResult> DbCheck([FromServices] ApplicationDbContext context)
        {
            var count = await context.Experiences.CountAsync();
            return Ok($"Esperienze nel DB: {count}");
        }
    }
}
