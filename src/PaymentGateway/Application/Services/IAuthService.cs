namespace Application.Services
{
    using Application.Models;

    public interface IAuthService
    {
        BaseResult<AuthResult> AuthenticateUser(AuthRequest authRequest);
    }
}