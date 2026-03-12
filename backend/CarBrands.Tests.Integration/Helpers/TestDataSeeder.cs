using CarBrands.Domain.Entities;
using CarBrands.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarBrands.Tests.Integration.Helpers;

public static class TestDataSeeder
{
    public static async Task SeedMarcasAutosAsync(CarBrandsContext context)
    {
        if (await context.MarcaAutos.AnyAsync())
            return;

        var marcas = new List<MarcaAuto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Toyota",
                OriginCountry = "Japón",
                FoundingDate = new DateTimeOffset(1937, 8, 28, 0, 0, 0, TimeSpan.Zero),
                WebSite = "https://www.toyota.com",
                Value = 250_000_000
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Ford",
                OriginCountry = "Estados Unidos",
                FoundingDate = new DateTimeOffset(1903, 6, 16, 0, 0, 0, TimeSpan.Zero),
                WebSite = "https://www.ford.com",
                Value = 200_000_000
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "BMW",
                OriginCountry = "Alemania",
                FoundingDate = new DateTimeOffset(1916, 3, 7, 0, 0, 0, TimeSpan.Zero),
                WebSite = "https://www.bmw.com",
                Value = 180_000_000
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Hyundai",
                OriginCountry = "Korea del Sur",
                FoundingDate = new DateTimeOffset(1967, 12, 29, 0, 0, 0, TimeSpan.Zero),
                WebSite = "https://www.hyundai.com",
                Value = 150_000_000
            }
        };

        context.MarcaAutos.AddRange(marcas);

        await context.SaveChangesAsync();
    }
}
