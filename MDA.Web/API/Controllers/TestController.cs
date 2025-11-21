using Microsoft.AspNetCore.Mvc;

namespace MDA.Web.API.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] string? type = null)
        {
            // Dummy payload: [{ key, value }, ...]
            var data = new[]
            {
                new { key = "timestamp", value = DateTimeOffset.UtcNow.ToString("O") },
                new { key = "type", value = type ?? "unknown" },
                new { key = "message", value = "dummy historical record" }
            };
            return Ok(data);
        }
    }
}
