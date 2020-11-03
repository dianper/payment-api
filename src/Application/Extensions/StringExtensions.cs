namespace Application.Extensions
{
    public static class StringExtensions
    {
        public static string ToMask(this string source)
        {
            return string.Concat("#### #### #### ", source.Substring(12));
        }
    }
}
