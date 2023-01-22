using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ArmedMFG.ApplicationCore.Entities.BasketAggregate;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using CatalogBrand = ArmedMFG.ApplicationCore.Entities.CatalogBrand;
using CatalogItem = ArmedMFG.ApplicationCore.Entities.CatalogItem;
using CatalogType = ArmedMFG.ApplicationCore.Entities.CatalogType;

namespace ArmedMFG.Infrastructure.Data;

public class CatalogContext : DbContext
{
    #pragma warning disable CS8618 // Required by Entity Framework
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) {}

    public DbSet<Basket> Baskets { get; set; }
    public DbSet<CatalogItem> CatalogItems { get; set; }
    public DbSet<CatalogBrand> CatalogBrands { get; set; }
    public DbSet<CatalogType> CatalogTypes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
