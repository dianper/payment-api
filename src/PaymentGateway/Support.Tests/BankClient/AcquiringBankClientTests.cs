namespace Support.Tests.BankClient
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Moq.Protected;
    using Support.BankClient;
    using Support.Configuration;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class AcquiringBankClientTests
    {
        private readonly Mock<IHttpClientFactory> httpClientFactoryMock;
        private readonly Mock<ILogger<AcquiringBankClient>> loggerMock;
        private readonly BankConfiguration bankConfiguration;
        private readonly AcquiringBankClient target;

        public AcquiringBankClientTests()
        {
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{'transactionId':'65a83e69-6741-4970-ab7c-433beb3837cb','transactionStatus':'ok'}"),
                });

            var httpClient = new HttpClient(httpMessageHandlerMock.Object);
            httpClient.BaseAddress = new Uri("https://localhost");

            this.httpClientFactoryMock = new Mock<IHttpClientFactory>();
            this.httpClientFactoryMock
                .Setup(s => s.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            this.loggerMock = new Mock<ILogger<AcquiringBankClient>>();

            this.bankConfiguration = new BankConfiguration
            {
                BaseAddress = "https://localhost",
                Endpoint = "/api/endpoint",
                ServiceName = "serviceName",
                Timeout = 1000
            };

            this.target = new AcquiringBankClient(this.httpClientFactoryMock.Object, this.loggerMock.Object, this.bankConfiguration);
        }

        [Fact]
        public async Task ProcessTransactionAsync_HappyJourney_ReturnsTransaction()
        {
            // Arrange
            var bankClientRequest = new BankClientRequest
            {
                Amount = 1000,
                CardNumber = "1234567890123456",
                ExpiryMonth = 12,
                ExpiryYear = 2020,
                SecurityCode = 123
            };

            // Act
            var bankResult = await this.target.ProcessTransactionAsync(bankClientRequest);

            // Assert
            Assert.NotNull(bankResult);
            Assert.Equal(new Guid("65a83e69-6741-4970-ab7c-433beb3837cb"), bankResult.TransactionId);

            this.httpClientFactoryMock
                .Verify(v => v.CreateClient(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ProcessTransactionAsync_ThrowsException_ReturnsDefault()
        {
            // Arrange
            var bankClientRequest = new BankClientRequest
            {
                Amount = 1000,
                CardNumber = "1234567890123456",
                ExpiryMonth = 12,
                ExpiryYear = 2020,
                SecurityCode = 123
            };

            this.httpClientFactoryMock
                .Setup(s => s.CreateClient(It.IsAny<string>()))
                .Throws(new Exception());

            // Act
            var bankResult = await this.target.ProcessTransactionAsync(bankClientRequest);

            // Assert
            Assert.Null(bankResult);

            this.httpClientFactoryMock
                .Verify(v => v.CreateClient(It.IsAny<string>()), Times.Once);
            this.loggerMock
                .Verify(v => v.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Equals("Error while processing bank transaction.")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }
    }
}