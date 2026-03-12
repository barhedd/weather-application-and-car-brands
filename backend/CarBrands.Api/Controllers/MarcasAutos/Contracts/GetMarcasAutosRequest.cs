using CarBrands.Api.Commons.Contracts;

namespace CarBrands.Api.Controllers.MarcasAutos.Contracts;

public sealed class GetMarcasAutosRequest : PagedQueryRequest
{
    // filters
    public string? Name { get; init; }
    public string? OriginCountry { get; init; }
    public DateTimeOffset? FoundingDate { get; init; }
    public decimal? Value { get; init; }
}
