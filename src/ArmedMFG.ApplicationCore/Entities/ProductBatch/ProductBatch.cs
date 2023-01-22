﻿using System;
using System.Collections.Generic;
using System.Linq;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.ProductBatch;

public class ProductBatch : BaseEntity, IAggregateRoot
{
    public DateTime ProducedDate { get; set; }

    private readonly List<ProducedProduct> _producedProducts = new List<ProducedProduct>();
    private readonly List<SpentMaterial> _spentMaterials = new List<SpentMaterial>();
    
    public IReadOnlyCollection<ProducedProduct> ProducedProducts => _producedProducts.AsReadOnly();
    public IReadOnlyCollection<SpentMaterial> SpentMaterials => _spentMaterials.AsReadOnly();
    
    public ProductBatch(DateTime producedDate)
    {
        ProducedDate = producedDate;
    }
    
    public void AddProduct(int productTypeId,
        int quantity)
    {
        if (!ProducedProducts.Any(p => p.ProductTypeId == productTypeId))
        {
            _producedProducts.Add(new ProducedProduct(productTypeId, Id, quantity));
            return;
        }
        var existingProduct = ProducedProducts.First(p => p.ProductTypeId == productTypeId);
        existingProduct.AddQuantity(quantity);
    }
    
    public void AddMaterial(int materialTypeId,
        float amount)
    {
        if (!SpentMaterials.Any(m => m.MaterialTypeId == materialTypeId))
        {
            _spentMaterials.Add(new SpentMaterial(materialTypeId, Id, amount));
            return;
        }
        var existingMaterial = SpentMaterials.First(m => m.MaterialTypeId == materialTypeId);
        existingMaterial.AddQuantity(amount);
    }

    public void AddMaterialRange(IList<SpentMaterial> spentMaterials)
    {
        foreach (var material in spentMaterials) 
        {
            _spentMaterials.Add(material);
        }
    }

    public void AddProductRange(IList<ProducedProduct> producedProducts)
    {
        
        foreach (var product in producedProducts) 
        {
            _producedProducts.Add(product);
        }
    } 
    
    public void RemoveEmptyProducts()
    {
        _producedProducts.RemoveAll(p => p.Quantity == 0);
    }

    public void RemoveEmptyMaterials()
    {
        _spentMaterials.RemoveAll(r => r.Amount == 0);
    }
}
