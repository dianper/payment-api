namespace Application.Tests.Extensions
{
    using Application.Extensions;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("123456")]
        public void ToCardMasked_WhenSourceIsNullOrEmptyOrLengthLessThanMaskLength_ReturnsDefault(string cardNumber)
        {
            // Act & Assert
            Assert.Null(cardNumber.ToCardMasked());
        }

        [Fact]
        public void ToCardMasked_HappyJourney_ReturnsMasked()
        {
            // Arrange
            var cardNumber = "1234567890123456";

            // Act
            var masked = cardNumber.ToCardMasked();

            // Assert
            Assert.NotNull(masked);
            Assert.StartsWith("************", masked);
        }
    }
}