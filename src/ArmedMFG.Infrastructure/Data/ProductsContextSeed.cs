using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities;
using ArmedMFG.ApplicationCore.Entities.CustomerAggregate;
using ArmedMFG.ApplicationCore.Entities.EmployeeAggregate;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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


            if (!await productsContext.ProductCategories.AnyAsync())
            {
                await productsContext.ProductCategories.AddRangeAsync(
                    GetPreconfiguredProductCategories());

                await productsContext.SaveChangesAsync();
            }

            if (!await productsContext.Products.AnyAsync())
            {
                await productsContext.Products.AddRangeAsync(
                    GetPreconfiguredProducts());

                await productsContext.SaveChangesAsync();
            }

            if (!await productsContext.MaterialCategories.AnyAsync())
            {
                await productsContext.MaterialCategories.AddRangeAsync(
                    GetPreconfiguredMaterialCategories());
                
                await productsContext.SaveChangesAsync();
            }
            
            if (!await productsContext.Materials.AnyAsync())
            {
                await productsContext.Materials.AddRangeAsync(
                    GetPreconfiguredMaterials());
                
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

           
            if (!await productsContext.EmployeePositions.AnyAsync())
            {
                await productsContext.EmployeePositions.AddRangeAsync(
                    GetPreconfiguredEmployeePositions());
            }

            if (!await productsContext.Employees.AnyAsync())
            {
                await productsContext.Employees.AddRangeAsync(
                    GetPreconfiguredEmployees());
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
            new("ЖБИ"),
            new("Механический цех"),
        };
    }

    static IEnumerable<ProductCategory> GetPreconfiguredProductCategories()
    {
        return new List<ProductCategory>
        {
            new("Плиты Перекрытия"),
            new("Лотки"),
            new("Кольца Стеновые"),
        };
    }
    
    static IEnumerable<Product> GetPreconfiguredProducts()
    {
        return new List<Product>
        {
            new(1, "ПК 59x10", 30, 1350000),
            new(1, "ПК 59x12", 35, 1350000),
            new(1, "ПК 45x10", 39, 1350000),
            new(1, "ПК 45x12", 39, 1350000),
            new(1, "ПК 60x10", 39, 1350000),
            new(1, "ПК 60x12", 39, 1350000),
        };
    }

    static IEnumerable<EmployeePosition> GetPreconfiguredEmployeePositions() 
    {
        return new List<EmployeePosition>
        {
            new("Сварщик", EmployeeSalaryType.PerformanceBased),
            new("Программист", EmployeeSalaryType.Fixed)
        };
    }

    static IEnumerable<Employee> GetPreconfiguredEmployees()
    {
        return new List<Employee>
        {
            new("Рустам Минниханов Фанисович", "+998942588311", DateTime.ParseExact("02/09/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTime.Now, 2, EmployeeStatus.Active),
        };
    }

    static IEnumerable<MaterialCategory> GetPreconfiguredMaterialCategories()
    {
        return new List<MaterialCategory>
        {
            new("Жидкость"),
            new("Песок"),
            new("Щебень"),
            new("Цемент"),
            new("Арматура")
        };
    }

    static IEnumerable<Material> GetPreconfiguredMaterials()
    {
        return new List<Material>
        {
            new(1, "Вода", Unit.Liter, 1000),
            new(4, "Цемент", Unit.Kilogram, 1000),
            new(5, "Арматура 4", Unit.Kilogram, 1000),
            new(5, "Арматура 8", Unit.Kilogram, 1000),
            new(5, "Арматура 12", Unit.Kilogram, 1000),
            new(3, "Щебень 6", Unit.Kilogram, 1000)
        };
    }

    static IEnumerable<Customer> GetPreconfiguredCustomers()
    {
        return new List<Customer>
        {
            new Customer(
                "Абдувахоб Холиков",
                "+998905001020",
                CustomerPosition.Manager,
                "abduvakhob_kholikov@gmail.com", 
                true,
                "3092939190239901239",
                "Martur Fompak International",
                FindOutThrough.ThroughPartnets,
                "Мужчина в возрасте ездит на ласети номер 01АА302ОА"
                ),
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
            new(DateTime.Now.AddDays(-1), 1, 1, PaymentMethod.CashWithoutVat, "Отплата заказа", 10000000),
        };
    } 
}
