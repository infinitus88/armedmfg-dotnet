using System.Reflection;
using ArmedMFG.ApplicationCore.Entities.CustomerAggregate;
using ArmedMFG.ApplicationCore.Entities.EmployeeAggregate;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;
// using ArmedMFG.ApplicationCore.Entities.OrderCatalogAggregate;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductionReport;
using Microsoft.EntityFrameworkCore;

namespace ArmedMFG.Infrastructure.Data;

#pragma warning disable CS8618
public class ProductsContext : DbContext
{
#pragma warning disable CS8618 // Required by Entity Framework
    public ProductsContext(DbContextOptions<ProductsContext> options) : base(options) {}
    
    // Produced Products
    public DbSet<ProductionReport> ProductionReports { get; set; }
    public DbSet<ProducedProduct> ProducedProducts { get; set; }
    
    // Products
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    
    // Payments
    public DbSet<PaymentRecord> PaymentRecords { get; set; }
    public DbSet<PaymentCategory> PaymentCategories { get; set; }

    // Materials
    public DbSet<Material> Materials { get; set; }
    public DbSet<MaterialCategory> MaterialCategories { get; set; }
    public DbSet<MaterialSupply> MaterialSupplies { get; set; }

    // Employees
    public DbSet<EmployeePosition> EmployeePositions { get; set; }
    public DbSet<Employee> Employees { get; set; }
    
    // Customers
    public DbSet<Customer> Customers { get; set; }
    
    // Orders
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderShipment> OrderShipments { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    } 
}
