using CarBrands.Application.Common.Pagination;
using CarBrands.Application.Features.MarcasAutos.Dtos;
using CarBrands.Application.Features.MarcasAutos.Interfaces;
using CarBrands.Application.Features.MarcasAutos.Queries.GetMarcasAutos;
using CarBrands.Domain.Entities;
using CarBrands.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarBrands.Infrastructure.Persistence.Repositories;

public class MarcaAutoRepository(CarBrandsContext dbContext) : IMarcaAutoRepository
{
    private readonly CarBrandsContext _dbContext = dbContext;

    public async Task<PagedResult<MarcaAutoResponseDto>> GetPagedAsync(GetMarcasAutosQuery query, CancellationToken ct)
    {
        IQueryable<MarcaAuto> marcasQuery = _dbContext.MarcaAutos
        .AsNoTracking();

        // Filters

        if (!string.IsNullOrWhiteSpace(query.Name))
        {
            marcasQuery = marcasQuery.Where(m =>
                EF.Functions.ILike(m.Name, $"%{query.Name}%"));
        }

        if (!string.IsNullOrWhiteSpace(query.OriginCountry))
        {
            marcasQuery = marcasQuery.Where(m =>
                EF.Functions.ILike(m.OriginCountry, $"%{query.OriginCountry}%"));
        }

        if (query.FoundingDate.HasValue)
        {
            marcasQuery = marcasQuery.Where(m =>
                m.FoundingDate.Date == query.FoundingDate.Value.Date);
        }

        if (query.Value.HasValue)
        {
            marcasQuery = marcasQuery.Where(m =>
                m.Value == query.Value.Value);
        }

        // Sorting

        marcasQuery = ApplySorting(marcasQuery, query);

        // Total records before pagination

        var totalRecords = await marcasQuery.CountAsync(ct);

        // Pagination

        var data = await marcasQuery
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(m => new MarcaAutoResponseDto
            {
                Id = m.Id,
                Name = m.Name,
                OriginCountry = m.OriginCountry,
                FoundingDate = m.FoundingDate,
                WebSite = m.WebSite,
                Value = m.Value
            })
            .ToListAsync(ct);

        return new PagedResult<MarcaAutoResponseDto>(
            data,
            query.PageNumber,
            query.PageSize,
            totalRecords);
    }

    private static IQueryable<MarcaAuto> ApplySorting(
        IQueryable<MarcaAuto> query,
        GetMarcasAutosQuery request)
    {
        var direction = request.SortDirection?.ToLower() == "desc";

        return request.SortBy?.ToLower() switch
        {
            "name" => direction
                ? query.OrderByDescending(x => x.Name)
                : query.OrderBy(x => x.Name),

            "origincountry" => direction
                ? query.OrderByDescending(x => x.OriginCountry)
                : query.OrderBy(x => x.OriginCountry),

            "foundingdate" => direction
                ? query.OrderByDescending(x => x.FoundingDate)
                : query.OrderBy(x => x.FoundingDate),

            "value" => direction
                ? query.OrderByDescending(x => x.Value)
                : query.OrderBy(x => x.Value),

            _ => query.OrderBy(x => x.Name)
        };
    }
}
