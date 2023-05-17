using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.ProductAggregate;

public class Product : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public int ProductCategoryId { get; private set; }
    public ProductCategory? ProductCategory { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; set; }
     
    private readonly List<ProductMaterial> _productMaterials = new List<ProductMaterial>();
    public IReadOnlyCollection<ProductMaterial> ProductMaterials => _productMaterials.AsReadOnly();
    
    public Product(int productCategoryId,
        string name,
        int quantity,
        decimal unitPrice
        )
    {
        ProductCategoryId = productCategoryId;
        Name = name;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public void AddRangeProductMaterials(IEnumerable<ProductMaterial> productMaterials)
    {
        foreach (var material in productMaterials)
        {
            _productMaterials.Add(material);
        }
    }

    public void UpdateDetails(ProductDetails details)
    {
        Guard.Against.NullOrEmpty(details.Name, nameof(details.Name));
        Guard.Against.NegativeOrZero(details.UnitPrice, nameof(details.UnitPrice));

        Name = details.Name;
        UnitPrice = details.UnitPrice;
        UpdateCategory(details.ProductCategoryId);
    }

    public void UpdateCategory(int productCategoryId)
    {
        Guard.Against.Zero(productCategoryId, nameof(productCategoryId));
        ProductCategoryId = productCategoryId;
    }

    public readonly record struct ProductDetails
    {
        public string? Name { get; }
        public decimal UnitPrice { get; }
        public int ProductCategoryId { get; }

        public ProductDetails(string? name, int productCategoryId, decimal unitPrice)
        {
            Name = name;
            UnitPrice = unitPrice;
            ProductCategoryId = productCategoryId;
        }
    }
}
