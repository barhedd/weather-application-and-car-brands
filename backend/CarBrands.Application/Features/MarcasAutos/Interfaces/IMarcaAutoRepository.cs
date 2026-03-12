using CarBrands.Application.Common.Pagination;
using CarBrands.Application.Features.MarcasAutos.Dtos;
using CarBrands.Application.Features.MarcasAutos.Queries.GetMarcasAutos;

namespace CarBrands.Application.Features.MarcasAutos.Interfaces;

public interface IMarcaAutoRepository
{
    Task<PagedResult<MarcaAutoResponseDto>> GetPagedAsync(GetMarcasAutosQuery query, CancellationToken ct);
}
