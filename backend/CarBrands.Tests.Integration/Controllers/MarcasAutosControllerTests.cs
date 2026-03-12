using CarBrands.Api.Controllers.MarcasAutos;
using CarBrands.Api.Controllers.MarcasAutos.Contracts;
using CarBrands.Application.Common.Pagination;
using CarBrands.Application.Features.MarcasAutos.Dtos;
using CarBrands.Application.Features.MarcasAutos.Queries.GetMarcasAutos;
using CarBrands.Infrastructure.Persistence.Repositories;
using CarBrands.Tests.Integration.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBrands.Tests.Integration.Controllers;

public class MarcasAutosControllerTests
{
    [Fact]
    public async Task Get_ShouldReturnAllCarBrands()
    {
        // Arrange
        var context = DbContextFactory.Create();

        await TestDataSeeder.SeedMarcasAutosAsync(context);

        var repository = new MarcaAutoRepository(context);
        var handler = new GetMarcasAutosQueryHandler(repository);
        var controller = new MarcasAutosController(handler);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        var query = new GetMarcasAutosRequest
        {
            PageNumber = 1,
            PageSize = 10
        };

        // Act
        var result = await controller.Get(query);

        // Assert
        var okResult = result as OkObjectResult;

        okResult.Should().NotBeNull();

        var pagedResult = okResult!.Value as PagedResult<MarcaAutoResponseDto>;

        pagedResult.Should().NotBeNull();
        pagedResult!.TotalRecords.Should().Be(4);
    }
}
