using CarBrands.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;

namespace CarBrands.Tests.Integration.Helpers;

public static class DbContextFactory
{
    public static CarBrandsContext Create()
    {
        var options = new DbContextOptionsBuilder<CarBrandsContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new CarBrandsContext(options);

        context.Database.EnsureCreated();

        return context;
    }
}
