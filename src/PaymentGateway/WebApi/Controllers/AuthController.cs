namespace WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/v1/auth")]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("token");
        }
    }
}