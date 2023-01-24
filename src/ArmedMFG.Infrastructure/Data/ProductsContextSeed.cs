using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
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
            }
            
            if (!await productsContext.MaterialTypes.AnyAsync())
            {
                await productsContext.MaterialTypes.AddRangeAsync(
                    GetPreconfiguredMaterialTypes());
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
            new(1, "PK 59x10 10", "Plates that are used for base of every floor buildings", "http://catalogbaseurltobereplaced/images/products/1.png"),
            new(1, "PK 59x12 10", "Plates that are used for base of every floor buildings", "http://catalogbaseurltobereplaced/images/products/1.png"),
            new(1, "PK 59x15 10", "Plates that are used for base of every floor buildings", "http://catalogbaseurltobereplaced/images/products/1.png"),
            new(1, "PK 59x16 10", "Plates that are used for base of every floor buildings", "http://catalogbaseurltobereplaced/images/products/1.png"),
            new(1, "PK 59x17 10", "Plates that are used for base of every floor buildings", "http://catalogbaseurltobereplaced/images/products/1.png"),
            new(1, "PK 59x19 10", "Plates that are used for base of every floor buildings", "http://catalogbaseurltobereplaced/images/products/1.png"),
            new(2, "LT 10x08 10", "Lotocs that are used for water outline channels", "http://catalogbaseurltobereplaced/images/products/1.png"),
            new(2, "LT 10x08 10", "Lotocs that are used for water outline channels", "http://catalogbaseurltobereplaced/images/products/1.png"),
            new(2, "LT 10x08 10", "Lotocs that are used for water outline channels", "http://catalogbaseurltobereplaced/images/products/1.png"),
            new(2, "LT 10x08 10", "Lotocs that are used for water outline channels", "http://catalogbaseurltobereplaced/images/products/1.png"),
            new(2, "LT 10x08 10", "Lotocs that are used for water outline channels", "http://catalogbaseurltobereplaced/images/products/1.png"),
            new(2, "LT 10x08 10", "Lotocs that are used for water outline channels", "http://catalogbaseurltobereplaced/images/products/1.png"),
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
}
