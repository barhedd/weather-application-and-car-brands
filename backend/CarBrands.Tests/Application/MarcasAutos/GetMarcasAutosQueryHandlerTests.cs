using CarBrands.Application.Common.Pagination;
using CarBrands.Application.Features.MarcasAutos.Dtos;
using CarBrands.Application.Features.MarcasAutos.Interfaces;
using CarBrands.Application.Features.MarcasAutos.Queries.GetMarcasAutos;
using FluentAssertions;
using Moq;

namespace CarBrands.Tests.Application.MarcasAutos;

public class GetMarcasAutosQueryHandlerTests
{
    private readonly Mock<IMarcaAutoRepository> _repositoryMock;
    private readonly GetMarcasAutosQueryHandler _handler;

    public GetMarcasAutosQueryHandlerTests()
    {
        _repositoryMock = new Mock<IMarcaAutoRepository>();
        _handler = new GetMarcasAutosQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPagedResultWrappedInResultOk()
    {
        // Arrange
        var query = new GetMarcasAutosQuery
        {
            PageNumber = 1,
            PageSize = 10
        };

        var expectedPagedResult = new PagedResult<MarcaAutoResponseDto>(
            data:
            [
                new MarcaAutoResponseDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Toyota",
                    OriginCountry = "Japan",
                    FoundingDate = new DateTimeOffset(1937, 8, 28, 0, 0, 0, TimeSpan.Zero),
                    WebSite = "https://toyota.com",
                    Value = 200_000_000
                }
            ],
            pageNumber: 1,
            pageSize: 10,
            totalRecords: 1);

        _repositoryMock
            .Setup(x => x.GetPagedAsync(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPagedResult);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeEquivalentTo(expectedPagedResult);

        _repositoryMock.Verify(
            x => x.GetPagedAsync(query, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyPagedResult_WhenRepositoryReturnsEmpty()
    {
        // Arrange
        var query = new GetMarcasAutosQuery
        {
            PageNumber = 1,
            PageSize = 10
        };

        var emptyPagedResult = new PagedResult<MarcaAutoResponseDto>(
            data: [],
            pageNumber: 1,
            pageSize: 10,
            totalRecords: 0);

        _repositoryMock
            .Setup(x => x.GetPagedAsync(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(emptyPagedResult);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Data.Should().BeEmpty();
        result.Value.TotalRecords.Should().Be(0);
    }

    [Fact]
    public async Task Handle_ShouldPassCancellationToken_ToRepository()
    {
        // Arrange
        var query = new GetMarcasAutosQuery();
        var cancellationToken = new CancellationTokenSource().Token;

        _repositoryMock
            .Setup(x => x.GetPagedAsync(query, cancellationToken))
            .ReturnsAsync(new PagedResult<MarcaAutoResponseDto>(
                data: [],
                pageNumber: 1,
                pageSize: 10,
                totalRecords: 0));

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _repositoryMock.Verify(
            x => x.GetPagedAsync(query, cancellationToken),
            Times.Once);
    }
}
