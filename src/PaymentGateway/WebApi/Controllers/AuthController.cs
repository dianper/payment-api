namespace WebApi.Controllers
{
    using Application.Models;
    using Application.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<AuthResult> GenerateToken(AuthRequest authRequest)
        {
            var result = this.authService.AuthenticateUser(authRequest);
            if (result.Success)
            {
                return this.Ok(result);
            }

            return this.Unauthorized(result);
        }
    }
}