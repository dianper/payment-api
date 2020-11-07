namespace WebApi.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Application.Models;
    using Application.Services;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using WebApi.Controllers;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class PaymentControllerTests
    {
        private readonly Mock<IPaymentService> paymentServiceMock;
        private readonly PaymentController target;

        public PaymentControllerTests()
        {
            this.paymentServiceMock = new Mock<IPaymentService>();
            this.target = new PaymentController(this.paymentServiceMock.Object);
        }

        [Fact]
        public async Task PostAsync_Successful_ReturnsOk()
        {
            // Arrange
            var request = new PaymentPostRequest
            {
                Amount = 1000,
                CardNumber = "1234567890123456",
                Currency = "USD",
                ExpiryMonth = 10,
                ExpiryYear = 2021,
                MerchantId = Guid.NewGuid(),
                SecurityCode = 523
            };

            this.paymentServiceMock
                .Setup(s => s.ProcessPaymentAsync(request))
                .ReturnsAsync(new BaseResult<PaymentPostResult>(new PaymentPostResult(Guid.NewGuid(), Guid.NewGuid(), "ok")));

            // Act
            var result = await this.target.PostAsync(request);
            var objResult = result as OkObjectResult;
            var paymentResult = objResult.Value as BaseResult<PaymentPostResult>;

            // Assert
            Assert.NotNull(paymentResult.Result);
            Assert.True(paymentResult.Success);
            Assert.Empty(paymentResult.Errors);

            this.paymentServiceMock
                .Verify(v => v.ProcessPaymentAsync(request), Times.Once);
        }

        [Fact]
        public async Task PostAsync_Unsuccessful_ReturnsBadRequest()
        {
            // Arrange
            var request = new PaymentPostRequest
            {
                Amount = 1000,
                CardNumber = "1234567890123456",
                Currency = "USD",
                ExpiryMonth = 10,
                ExpiryYear = 2021,
                MerchantId = Guid.NewGuid(),
                SecurityCode = 523
            };

            this.paymentServiceMock
                .Setup(s => s.ProcessPaymentAsync(request))
                .ReturnsAsync(new BaseResult<PaymentPostResult>(errors: new Dictionary<string, string> { { "someError", "someDescription" } }));

            // Act
            var result = await this.target.PostAsync(request);
            var objResult = result as BadRequestObjectResult;
            var paymentResult = objResult.Value as BaseResult<PaymentPostResult>;

            // Assert
            Assert.Null(paymentResult.Result);
            Assert.False(paymentResult.Success);
            Assert.Single(paymentResult.Errors);

            this.paymentServiceMock
                .Verify(v => v.ProcessPaymentAsync(request), Times.Once);
        }

        [Fact]
        public async Task GetAsync_Successful_ReturnsOk()
        {
            // Arrange
            var request = new PaymentGetRequest
            {
                PaymentId = Guid.NewGuid()
            };

            this.paymentServiceMock
                .Setup(s => s.RetrievePaymentDetailsAsync(request))
                .ReturnsAsync(new BaseResult<PaymentGetResult>(new PaymentGetResult()));

            // Act
            var result = await this.target.GetAsync(request);
            var objResult = result as OkObjectResult;
            var paymentResult = objResult.Value as BaseResult<PaymentGetResult>;

            // Assert
            Assert.NotNull(paymentResult.Result);
            Assert.True(paymentResult.Success);
            Assert.Empty(paymentResult.Errors);

            this.paymentServiceMock
                .Verify(v => v.RetrievePaymentDetailsAsync(request), Times.Once);
        }

        [Fact]
        public async Task GetAsync_Unsuccessful_ReturnsNotFound()
        {
            // Arrange
            var request = new PaymentGetRequest
            {
                PaymentId = Guid.NewGuid()
            };

            this.paymentServiceMock
                .Setup(s => s.RetrievePaymentDetailsAsync(request))
                .ReturnsAsync(new BaseResult<PaymentGetResult>(errors: new Dictionary<string, string> { { "someError", "someDescription" } }));

            // Act
            var result = await this.target.GetAsync(request);
            var objResult = result as NotFoundObjectResult;
            var paymentResult = objResult.Value as BaseResult<PaymentGetResult>;

            // Assert
            Assert.Null(paymentResult.Result);
            Assert.False(paymentResult.Success);
            Assert.Single(paymentResult.Errors);

            this.paymentServiceMock
                .Verify(v => v.RetrievePaymentDetailsAsync(request), Times.Once);
        }
    }
}