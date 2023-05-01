using System.Reflection;
using ArmedMFG.ApplicationCore.Entities;
using ArmedMFG.ApplicationCore.Entities.BasketAggregate;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using ArmedMFG.ApplicationCore.Entities.EmployeeAggregate;
using ArmedMFG.ApplicationCore.Entities.MaterialStockAggregate;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
// using ArmedMFG.ApplicationCore.Entities.OrderCatalogAggregate;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using ArmedMFG.ApplicationCore.Entities.ProductStockAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using Microsoft.EntityFrameworkCore;

namespace ArmedMFG.Infrastructure.Data;

#pragma warning disable CS8618
public class ProductsContext : DbContext
{
#pragma warning disable CS8618 // Required by Entity Framework
    public ProductsContext(DbContextOptions<ProductsContext> options) : base(options) {}
    
    // TODO Remove this entities after testing
    // public DbSet<Basket> Baskets { get; set; }
    // public DbSet<CatalogItem> CatalogItems { get; set; }
    // public DbSet<CatalogBrand> CatalogBrands { get; set; }
    // public DbSet<CatalogType> CatalogTypes { get; set; }
    // public DbSet<Order> Orders { get; set; }
    // public DbSet<OrderItem> OrderItems { get; set; }
    // public DbSet<BasketItem> BasketItems { get; set; } 
    
    // Produced Products
    public DbSet<ProductBatch> ProductBatches { get; set; }
    public DbSet<ProducedProduct> ProducedProducts { get; set; }
    public DbSet<DefectiveProduct> DefectiveProducts { get; set; }
    public DbSet<SpentMaterial> SpentMaterials { get; set; }
    public DbSet<Department> Departments { get; set; }
    
    // Products
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductPrice> ProductPrices { get; set; }
    
    // Payments
    public DbSet<PaymentRecord> PaymentRecords { get; set; }
    public DbSet<PaymentCategory> PaymentCategories { get; set; }

    // Materials
    public DbSet<MaterialType> MaterialTypes { get; set; }
    public DbSet<MaterialCategory> MaterialCategories { get; set; }
    public DbSet<MaterialSupply> MaterialSupplies { get; set; }

    // Employees
    public DbSet<EmployeePosition> EmployeePositions { get; set; }
    public DbSet<Employee> Employees { get; set; }
    
    // Customers
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerOrganization> CustomerOrganizations { get; set; }
    
    // Orders
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderShipment> OrderShipments { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    
    // Warehouse
    public DbSet<ProductCheckPoint> ProductCheckPoints { get; set; }
    public DbSet<MaterialCheckPoint> MaterialCheckPoints { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    } 
}
