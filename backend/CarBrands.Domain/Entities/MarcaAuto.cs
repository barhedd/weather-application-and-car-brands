namespace CarBrands.Domain.Entities;

public class MarcaAuto : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string OriginCountry { get; set; } = string.Empty;
    public DateTimeOffset FoundingDate { get; set; }
    public string WebSite { get; set; } = string.Empty;
    public decimal Value { get; set; }
}
