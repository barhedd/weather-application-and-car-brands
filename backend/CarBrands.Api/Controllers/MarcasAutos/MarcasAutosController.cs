using CarBrands.Api.Commons.Extensions;
using CarBrands.Api.Controllers.MarcasAutos.Contracts;
using CarBrands.Application.Common.Abstractions.Events;
using CarBrands.Application.Common.Pagination;
using CarBrands.Application.Features.MarcasAutos.Dtos;
using CarBrands.Application.Features.MarcasAutos.Queries.GetMarcasAutos;
using Microsoft.AspNetCore.Mvc;

namespace CarBrands.Api.Controllers.MarcasAutos;

[ApiController]
[Route("api/[controller]")]
public class MarcasAutosController(
    IQueryHandler<GetMarcasAutosQuery, PagedResult<MarcaAutoResponseDto>> getMarcasAutosHandler) : ControllerBase
{
    private readonly IQueryHandler<GetMarcasAutosQuery, PagedResult<MarcaAutoResponseDto>> _getMarcasAutosHandler = getMarcasAutosHandler;

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetMarcasAutosRequest request)
    {
        var query = new GetMarcasAutosQuery
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            SortBy = request.SortBy,
            SortDirection = request.SortDirection,
            Name = request.Name,
        };

        var result = await _getMarcasAutosHandler.Handle(
            query,
            HttpContext.RequestAborted);

        if (result.Failure)
            return result.ToFailureResult();

        return Ok(result.Value);
    }
}
