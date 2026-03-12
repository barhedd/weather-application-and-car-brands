using CarBrands.Domain.Entities;
using CarBrands.Infrastructure.Contexts;
using System.Text.Json;

namespace CarBrands.Infrastructure.DataSeed;

public static class DataSeedHelper
{
    public static async Task SeedAsync(this CarBrandsContext dbContext)
    {
        // --- MatchStatuses ---
        if (!dbContext.MarcaAutos.Any())
        {
            var carBrands = LoadJson<MarcaAuto>("carBrands.json");
            await dbContext.MarcaAutos.AddRangeAsync(carBrands);
            await dbContext.SaveChangesAsync();
        }
    }

    private static List<T> LoadJson<T>(string fileName)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "DataSeed", fileName);
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<T>>(json) ?? [];
    }
}
