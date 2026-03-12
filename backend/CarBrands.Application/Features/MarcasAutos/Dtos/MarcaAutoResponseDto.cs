namespace CarBrands.Application.Features.MarcasAutos.Dtos;

public record MarcaAutoResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string OriginCountry {  get; init; } = string.Empty;
    public DateTimeOffset FoundingDate {  get; init; }
    public string WebSite {  get; init; } = string.Empty;
    public decimal Value { get; init; }
}
