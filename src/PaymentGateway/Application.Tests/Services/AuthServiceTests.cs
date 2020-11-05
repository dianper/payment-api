namespace Application.Tests.Services
{    
    using Application.Models;
    using Application.Services;
    using Support.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class AuthServiceTests
    {
        private readonly AuthConfiguration authConfiguration;
        private readonly AuthService target;

        public AuthServiceTests()
        {
            this.authConfiguration = new AuthConfiguration
            {
                Audience = "http://localhost:12345",
                Issuer = "http://localhost:12345",
                Key = "checkoutpaymentgateway2020testkey"
            };

            this.target = new AuthService(authConfiguration);
        }

        [Fact]
        public void AuthenticateUser_WhenUserNotFound_ReturnsError()
        {
            // Arrange
            var authRequest = new AuthRequest
            {
                Username = "username",
                Password = "password"
            };

            // Act
            var result = this.target.AuthenticateUser(authRequest);

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Result);
            Assert.True(result.Errors.ContainsKey("authUser"));
        }

        [Fact]
        public void AuthenticateUser_HappyJourney_ReturnsToken()
        {
            // Arrange
            var authRequest = new AuthRequest
            {
                Username = "paymentgateway",
                Password = "2020"
            };

            // Act
            var result = this.target.AuthenticateUser(authRequest);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Result);
            Assert.Equal(0, result.Errors.Count);
        }
    }
}