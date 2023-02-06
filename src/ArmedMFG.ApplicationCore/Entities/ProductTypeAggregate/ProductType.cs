using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;

public class ProductType : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string PictureUri { get; private set; }
    public int ProductCategoryId { get; private set; }
    public ProductCategory? ProductCategory { get; private set; }
     
    private readonly List<ProductPrice> _productPrices = new List<ProductPrice>();
    public IReadOnlyCollection<ProductPrice> ProductPrices => _productPrices.AsReadOnly();
    
    public ProductType(int productCategoryId,
        string name,
        string description,
        string pictureUri)
    {
        ProductCategoryId = productCategoryId;
        Name = name;
        Description = description;
        PictureUri = pictureUri;
    }

    public ProductType(int productCategoryId,
        string name,
        string description,
        string pictureUri,
        decimal price)
    {
        ProductCategoryId = productCategoryId;
        Name = name;
        Description = description;
        PictureUri = pictureUri; 
        
        AddPriceRecord(DateTime.Now, price);
    }
    
    public void AddPriceRecord(DateTime fromDate, decimal price)
    {
        _productPrices.Add(new ProductPrice(Id, fromDate, price));
    }

    public decimal GetCurrentPrice()
    {
        var latestPrice = _productPrices.Where(p => p.FromDate <= DateTime.Now).MaxBy(p => p.FromDate);
        
        // TODO Return latest price
        return latestPrice == null ? 0 : latestPrice.Price;
    }

    public void UpdateDetails(ProductTypeDetails details)
    {
        Guard.Against.NullOrEmpty(details.Name, nameof(details.Name));
        Guard.Against.NullOrEmpty(details.Description, nameof(details.Description));

        Name = details.Name;
        Description = details.Description;
    }

    public void UpdateCategory(int productCategoryId)
    {
        Guard.Against.Zero(productCategoryId, nameof(productCategoryId));
        ProductCategoryId = productCategoryId;
    }

    public void UpdatePictureUri(string pictureName)
    {
        if (string.IsNullOrEmpty(pictureName))
        {
            PictureUri = string.Empty;
            return;
        }
        PictureUri = $"images\\products\\{pictureName}?{new DateTime().Ticks}";
    }

    public readonly record struct ProductTypeDetails
    {
        public string? Name { get; }
        public string? Description { get; }

        public ProductTypeDetails(string? name, string? description)
        {
            Name = name;
            Description = description;
        }
    }
}
