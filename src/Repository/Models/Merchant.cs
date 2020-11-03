namespace Repository.Models
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class Merchant : BaseEntity
    {
        public string Name { get; set; }
    }
}