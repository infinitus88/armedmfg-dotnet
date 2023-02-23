﻿using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;

public class MaterialType : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int MaterialCategoryId { get; private set; }
    public MaterialCategory? MaterialCategory { get; private set; }
    
    private readonly List<MaterialSupply> _materialSupplies = new List<MaterialSupply>();
    public IReadOnlyCollection<MaterialSupply> MaterialSupplies => _materialSupplies.AsReadOnly();

    
    public MaterialType(int materialCategoryId,
        string name,
        string description)
    {
        MaterialCategoryId = materialCategoryId;
        Name = name;
        Description = description;
    }

    public MaterialType(int materialCategoryId,
        string name,
        string description,
        double amount)
    {
        MaterialCategoryId = materialCategoryId;
        Name = name;
        Description = description;
            
        // AddSupplyDelivery(DateTime.Now, unitPrice, amount);
    }
    
    public void AddSupplyDelivery(DateTime deliveredDate, decimal price, double amount)
    {
        _materialSupplies.Add(new MaterialSupply(Id, deliveredDate, price, amount));
    }

    public void UpdateDetails(MaterialTypeDetails details)
    {
        Guard.Against.NullOrEmpty(details.Name, nameof(details.Name));
        Guard.Against.NullOrEmpty(details.Description, nameof(details.Description));

        Name = details.Name;
        Description = details.Description;
    }

    public void UpdateCategory(int materialCategoryId)
    {
        Guard.Against.Zero(materialCategoryId, nameof(materialCategoryId));
        MaterialCategoryId = materialCategoryId;
    }

    public readonly record struct MaterialTypeDetails
    {
        public string? Name { get; }
        public string? Description { get; }

        public MaterialTypeDetails(string? name, string? description)
        {
            Name = name;
            Description = description;
        }
    }
}
