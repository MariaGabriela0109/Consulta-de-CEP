using Microsoft.AspNetCore.Mvc;

namespace ConsultaCep.WebApi.Controllers
{
    [ApiController]
    [Route("health")] // endpoint /health
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // status HTTP 200 OK 
            return Ok(new { status = "API está online!" });
        }
    }
}