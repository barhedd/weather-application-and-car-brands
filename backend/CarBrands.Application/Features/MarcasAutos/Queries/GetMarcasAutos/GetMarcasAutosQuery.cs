using CarBrands.Application.Common.Pagination;

namespace CarBrands.Application.Features.MarcasAutos.Queries.GetMarcasAutos;

public sealed class GetMarcasAutosQuery : PagedQuery
{
    // Filters
    public string? Name { get; init; }
    public string? OriginCountry { get; init; }
    public DateTimeOffset? FoundingDate { get; init; }
    public decimal? Value { get; init; }
}
