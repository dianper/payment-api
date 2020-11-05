namespace Application.Extensions
{
    public static class StringExtensions
    {
        public static string ToCardMasked(this string cardNumber, char paddingChar = '*', int maskLength = 16)
        {
            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < maskLength) return default;

            return cardNumber.Substring(maskLength - 4).PadLeft(maskLength, paddingChar);
        }
    }
}