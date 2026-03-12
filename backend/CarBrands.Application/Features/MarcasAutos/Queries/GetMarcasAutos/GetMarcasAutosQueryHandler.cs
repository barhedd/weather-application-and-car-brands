using CarBrands.Application.Common.Abstractions.Events;
using CarBrands.Application.Common.Pagination;
using CarBrands.Application.Common.Results;
using CarBrands.Application.Features.MarcasAutos.Dtos;
using CarBrands.Application.Features.MarcasAutos.Interfaces;

namespace CarBrands.Application.Features.MarcasAutos.Queries.GetMarcasAutos;

public class GetMarcasAutosQueryHandler(
    IMarcaAutoRepository repository) : IQueryHandler<GetMarcasAutosQuery, PagedResult<MarcaAutoResponseDto>>
{
    private readonly IMarcaAutoRepository _repository = repository;

    public async Task<Result<PagedResult<MarcaAutoResponseDto>>> Handle(GetMarcasAutosQuery query, CancellationToken ct)
    {
        var pagedResult = await _repository.GetPagedAsync(query, ct);

        return Result<PagedResult<MarcaAutoResponseDto>>.Ok(pagedResult);
    }
}