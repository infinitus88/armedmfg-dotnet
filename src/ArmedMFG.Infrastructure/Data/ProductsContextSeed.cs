using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using ArmedMFG.ApplicationCore.Entities.MaterialStockAggregate;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductStockAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaymentType = ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate.PaymentType;

namespace ArmedMFG.Infrastructure.Data;

public class ProductsContextSeed
{
    public static async Task SeedAsync(ProductsContext productsContext,
        ILogger logger,
        int retry = 0)
    {
        var retryForAvailability = retry;
        try
        {
            if (productsContext.Database.IsSqlServer())
            {
                productsContext.Database.Migrate();
            }

            if (!await productsContext.Departments.AnyAsync())
            {
                await productsContext.Departments.AddRangeAsync(
                    GetPreconfiguredDepartments());

                await productsContext.SaveChangesAsync();
            }

            if (!await productsContext.ProductCategories.AnyAsync())
            {
                await productsContext.ProductCategories.AddRangeAsync(
                    GetPreconfiguredProductCategories());

                await productsContext.SaveChangesAsync();
            }

            if (!await productsContext.ProductTypes.AnyAsync())
            {
                await productsContext.ProductTypes.AddRangeAsync(
                    GetPreconfiguredProductTypes());

                await productsContext.SaveChangesAsync();
            }

            if (!await productsContext.MaterialCategories.AnyAsync())
            {
                await productsContext.MaterialCategories.AddRangeAsync(
                    GetPreconfiguredMaterialCategories());
                
                await productsContext.SaveChangesAsync();
            }
            
            if (!await productsContext.MaterialTypes.AnyAsync())
            {
                await productsContext.MaterialTypes.AddRangeAsync(
                    GetPreconfiguredMaterialTypes());
                
                await productsContext.SaveChangesAsync();
            }

            if (!await productsContext.ProductPrices.AnyAsync())
            {
                await productsContext.ProductPrices.AddRangeAsync(
                    GetPreconfiguredProductPrices());
                
                await productsContext.SaveChangesAsync();
            }

            if (!await productsContext.PaymentCategories.AnyAsync())
            {
                await productsContext.PaymentCategories.AddRangeAsync(
                    GetPreconfiguredPaymentCategories());

                await productsContext.SaveChangesAsync();
            }

            if (!await productsContext.PaymentRecords.AnyAsync())
            {
                await productsContext.PaymentRecords.AddRangeAsync(
                    GetPreconfiguredPaymentRecords());

                await productsContext.SaveChangesAsync();
            }

            if (!await productsContext.ProductCheckPoints.AnyAsync())
            {
                await productsContext.ProductCheckPoints.AddRangeAsync(
                    GetPreconfiguredProductCheckPoints());

                await productsContext.SaveChangesAsync();
            }

            if (!await productsContext.MaterialCheckPoints.AnyAsync())
            {
                await productsContext.MaterialCheckPoints.AddRangeAsync(
                    GetPreconfiguredMaterialCheckPoints());

                await productsContext.SaveChangesAsync();
            }

            if (!await productsContext.Customers.AnyAsync())
            {
                await productsContext.Customers.AddRangeAsync(
                    GetPreconfiguredCustomers());
                
                await productsContext.SaveChangesAsync();
            }

            if (!await productsContext.Orders.AnyAsync())
            {
                await productsContext.Orders.AddRangeAsync(
                    GetPreconfiguredOrders());
                
                await productsContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10) throw;

            retryForAvailability++;
            
            logger.LogError(ex.Message);
            await SeedAsync(productsContext, logger, retryForAvailability);
            throw;
        }
    }


    static IEnumerable<Department> GetPreconfiguredDepartments()
    {
        return new List<Department>
        {
            new("Concrete"),
            new("OtherDep1"),
            new("OtherDep2"),
            new("OtherDep3")
        };
    }

    static IEnumerable<ProductCategory> GetPreconfiguredProductCategories()
    {
        return new List<ProductCategory>
        {
            new(1, "Plates"),
            new(1, "Lotocs"),
            new(1, "Ariqs"),
            new(2, "Sicators")
        };
    }
    
    static IEnumerable<ProductType> GetPreconfiguredProductTypes()
    {
        return new List<ProductType>
        {
            new(1, "PK 59x10 10", "Plates that are used for base of every floor buildings", "http://catalogbaseurltobereplaced/images/products/PK_1.png"),
            new(1, "PK 59x12 10", "Plates that are used for base of every floor buildings", "http://catalogbaseurltobereplaced/images/products/PK_1.png"),
            new(1, "PK 59x15 10", "Plates that are used for base of every floor buildings", "http://catalogbaseurltobereplaced/images/products/PK_1.png"),
            new(1, "PK 59x16 10", "Plates that are used for base of every floor buildings", "http://catalogbaseurltobereplaced/images/products/PK_1.png"),
            new(1, "PK 59x17 10", "Plates that are used for base of every floor buildings", "http://catalogbaseurltobereplaced/images/products/PK_1.png"),
            new(1, "PK 59x19 10", "Plates that are used for base of every floor buildings", "http://catalogbaseurltobereplaced/images/products/PK_1.png"),
            new(2, "LT 10x08 10", "Lotocs that are used for water outline channels", "http://catalogbaseurltobereplaced/images/products/PK_1.png"),
            new(2, "LT 10x08 10", "Lotocs that are used for water outline channels", "http://catalogbaseurltobereplaced/images/products/PK_1.png"),
            new(2, "LT 10x08 10", "Lotocs that are used for water outline channels", "http://catalogbaseurltobereplaced/images/products/PK_1.png"),
            new(2, "LT 10x08 10", "Lotocs that are used for water outline channels", "http://catalogbaseurltobereplaced/images/products/PK_1.png"),
            new(2, "LT 10x08 10", "Lotocs that are used for water outline channels", "http://catalogbaseurltobereplaced/images/products/PK_1.png"),
            new(2, "LT 10x08 10", "Lotocs that are used for water outline channels", "http://catalogbaseurltobereplaced/images/products/PK_1.png"),
        };
    }

    static IEnumerable<MaterialCategory> GetPreconfiguredMaterialCategories()
    {
        return new List<MaterialCategory>
        {
            new(1, "Liquid"),
            new(1, "Dust"),
            new(1, "Metal"),
        };
    }

    static IEnumerable<MaterialType> GetPreconfiguredMaterialTypes()
    {
        return new List<MaterialType>
        {
            new(1, "Water", "Usual not distill water"),
            new(2, "Cement", "Cement for concrete products"),
            new(3, "Metal Sticks", "Metal sticks used for concrete products")
        };
    }

    

    static IEnumerable<ProductPrice> GetPreconfiguredProductPrices()
    {
        return new List<ProductPrice>
        {
            new(1, DateTime.Now, 10000),
            new(2, DateTime.Now, 100000)
        };
    }

    static IEnumerable<ProductCheckPoint> GetPreconfiguredProductCheckPoints()
    {
        return new List<ProductCheckPoint>
        {
            new ProductCheckPoint(DateTime.Now.AddDays(-5), 1, 50)
        };
    }

    static IEnumerable<MaterialCheckPoint> GetPreconfiguredMaterialCheckPoints()
    {
        return new List<MaterialCheckPoint>
        {
            new MaterialCheckPoint(DateTime.Now.AddDays(-5), 1, 50000)
        };
    }

    static IEnumerable<Customer> GetPreconfiguredCustomers()
    {
        return new List<Customer>
        {
            new Customer("Абдувахоб Холиков", "+998905001020", "Посредник", "abduvakhob_kholikov@gmail.com", "Рекламный банер Тойтепа", "Мужчина в возрасте, ездит на жентре черной, ценит качество, готов переплачивать за качество")
        };
    }

    static IEnumerable<Order> GetPreconfiguredOrders()
    {
        List<Order> orders = new List<Order>();

        var order1 = new Order(1, DateTime.Now.AddDays(-2), DateTime.Now.AddMonths(1), (decimal)100000000, "Уделить отдельное внимание прочности Плита Перекрытия");
        
        order1.AddOrderProduct(1, 100, (decimal)100000.00);

        OrderShipment orderShipment = new OrderShipment(DateTime.Now, "Минниханов Рустам", "+998942588311", "10HSA102", "Ташкентская область, Алмалык");
        orderShipment.AddShipmentProduct(1, 50);
        
        order1.AddPartialShipment(orderShipment);
        
        orders.Add(order1);

        return orders;
    }
    
    static IEnumerable<PaymentCategory> GetPreconfiguredPaymentCategories()
    {
        return new List<PaymentCategory>
        {
            // Base Income Categories
            new("Оплата Заказа", PaymentType.Income),
            
            // Base Expense Categories
            new("Зарплата Сотрудника", PaymentType.Expense),
            new("Покупка комплектующих", PaymentType.Expense)
        };
    }

    static IEnumerable<PaymentRecord> GetPreconfiguredPaymentRecords()
    {
        return new List<PaymentRecord>
        {
            new(DateTime.Now.AddDays(-1), 1, 1, (byte)PaymentMethod.CashWithoutVAT, "Отплата заказа", 10000000),
        };
    } 
}
