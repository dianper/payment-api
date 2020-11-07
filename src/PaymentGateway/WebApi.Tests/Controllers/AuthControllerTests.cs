namespace WebApi.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Application.Models;
    using Application.Services;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using WebApi.Controllers;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> authServiceMock;
        private readonly AuthController target;

        public AuthControllerTests()
        {
            this.authServiceMock = new Mock<IAuthService>();
            this.target = new AuthController(this.authServiceMock.Object);
        }

        [Fact]
        public void GenerateToken_Successful_ReturnsToken()
        {
            // Arrange
            this.authServiceMock
                .Setup(s => s.AuthenticateUser(It.IsAny<AuthRequest>()))
                .Returns(new BaseResult<AuthResult>(new AuthResult { Token = "token" }));

            // Act
            var result = this.target.GenerateToken(It.IsAny<AuthRequest>());
            var objResult = result.Result as OkObjectResult;
            var authResult = objResult.Value as BaseResult<AuthResult>;

            // Assert
            Assert.NotNull(authResult.Result);
            Assert.True(authResult.Success);
            Assert.Empty(authResult.Errors);
            Assert.Equal("token", authResult.Result.Token);
        }

        [Fact]
        public void GenerateToken_Successful_ReturnsUnauthorized()
        {
            // Arrange
            this.authServiceMock
                .Setup(s => s.AuthenticateUser(It.IsAny<AuthRequest>()))
                .Returns(new BaseResult<AuthResult>(errors: new Dictionary<string, string> { { "someError", "someDescription" } }));

            // Act
            var result = this.target.GenerateToken(It.IsAny<AuthRequest>());
            var objResult = result.Result as UnauthorizedObjectResult;
            var authResult = objResult.Value as BaseResult<AuthResult>;

            // Assert
            Assert.Null(authResult.Result);
            Assert.False(authResult.Success);
            Assert.Single(authResult.Errors);
        }
    }
}