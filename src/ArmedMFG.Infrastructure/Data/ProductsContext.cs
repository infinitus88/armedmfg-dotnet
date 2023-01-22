using System.Reflection;
using ArmedMFG.ApplicationCore.Entities;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using Microsoft.EntityFrameworkCore;

namespace ArmedMFG.Infrastructure.Data;

#pragma warning disable CS8618
public class ProductsContext : DbContext
{
#pragma warning disable CS8618 // Required by Entity Framework
    public ProductsContext(DbContextOptions<ProductsContext> options) : base(options) {}

    public DbSet<ProductBatch> ProductBatches { get; set; }
    public DbSet<ProducedProduct> ProducedProducts { get; set; }
    public DbSet<SpentMaterial> SpentMaterials { get; set; }
    
    public DbSet<Department> Departments { get; set; }
    
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductPriceHistory> ProductPriceHistory { get; set; }
    
    public DbSet<MaterialType> MaterialTypes { get; set; }
    public DbSet<MaterialCategory> MaterialCategories { get; set; }
    public DbSet<MaterialSupplyHistory> MaterialSupplyHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    } 
}
