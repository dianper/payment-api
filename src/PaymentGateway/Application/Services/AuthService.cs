namespace Application.Services
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using Application.Models;
    using Microsoft.IdentityModel.Tokens;
    using Support.Configuration;

    public class AuthService : IAuthService
    {
        private readonly AuthConfiguration authConfiguration;

        public AuthService(AuthConfiguration authConfiguration)
        {
            this.authConfiguration = authConfiguration;
        }

        public BaseResult<AuthResult> AuthenticateUser(AuthRequest authRequest)
        {
            var result = new BaseResult<AuthResult>();

            if (authRequest.Username.Equals("paymentgateway") && authRequest.Password.Equals("2020"))
            {
                result.Result = new AuthResult
                {
                    Token = this.GenerateToken(),
                    Expiration = DateTime.Now.AddMinutes(120)
                };

                return result;
            }

            result.Errors.Add("authUser", "User unauthorized.");
            return result;
        }

        private string GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.authConfiguration.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: this.authConfiguration.Issuer,
                audience: this.authConfiguration.Audience,
                claims: null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}