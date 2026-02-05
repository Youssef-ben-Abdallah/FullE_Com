using BI.Sales.Api.Entities.DataWarehouse;
using Microsoft.EntityFrameworkCore;

namespace BI.Sales.Api.Data.DataWarehouse;

public class DwSalesDbContext : DbContext
{
    public DwSalesDbContext(DbContextOptions<DwSalesDbContext> options) : base(options) { }

    public DbSet<KpiGlobal> KpiGlobal => Set<KpiGlobal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KpiGlobal>().HasNoKey().ToView("vw_KPIs_Global");
    }
}
