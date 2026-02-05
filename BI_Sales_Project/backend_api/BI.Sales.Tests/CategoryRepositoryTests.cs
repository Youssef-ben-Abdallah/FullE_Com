using BI.Sales.Api.Data.OltpEcommerce;
using BI.Sales.Api.Entities.OltpEcommerce;
using BI.Sales.Api.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BI.Sales.Tests;

public class CategoryRepositoryTests
{
    [Fact]
    public async Task AddAndGetCategory()
    {
        var options = new DbContextOptionsBuilder<OltpEcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        await using var context = new OltpEcommerceDbContext(options);
        var repo = new CategoryRepository(context);

        var category = await repo.AddAsync(new Category { Name = "Electronics" });
        var fetched = await repo.GetByIdAsync(category.CategoryId);

        Assert.NotNull(fetched);
        Assert.Equal("Electronics", fetched?.Name);
    }
}
