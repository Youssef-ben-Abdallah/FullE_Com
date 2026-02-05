using BI.Sales.Api.Entities.DataWarehouse;
using Microsoft.EntityFrameworkCore;

namespace BI.Sales.Api.Data.DataWarehouse;

public class DwSalesDbContext : DbContext
{
    public DwSalesDbContext(DbContextOptions<DwSalesDbContext> options) : base(options)
    {
    }

    public DbSet<KpiGlobal> KpiGlobals => Set<KpiGlobal>();
    public DbSet<SalesByPeriod> SalesByPeriods => Set<SalesByPeriod>();
    public DbSet<TopEntity> TopEntities => Set<TopEntity>();
    public DbSet<SalesByTerritory> SalesByTerritories => Set<SalesByTerritory>();
    public DbSet<SalesByShipMethod> SalesByShipMethods => Set<SalesByShipMethod>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KpiGlobal>().HasNoKey().ToView("vw_KPIs_Global");
        modelBuilder.Entity<SalesByPeriod>().HasNoKey();
        modelBuilder.Entity<TopEntity>().HasNoKey();
        modelBuilder.Entity<SalesByTerritory>().HasNoKey().ToView("vw_Sales_By_Territory");
        modelBuilder.Entity<SalesByShipMethod>().HasNoKey().ToView("vw_Sales_By_ShipMethod");
    }
}
