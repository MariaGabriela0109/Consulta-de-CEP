using Microsoft.AspNetCore.Mvc;

namespace ConsultaCep.WebApi.Controllers
{
    /// <summary>
    /// </summary>
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { status = "API está online!" });
        }
    }
}