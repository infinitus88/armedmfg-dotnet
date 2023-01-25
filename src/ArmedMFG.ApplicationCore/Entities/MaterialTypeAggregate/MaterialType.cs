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
    
    private readonly List<MaterialSupplyHistory> _materialSupplyHistory = new List<MaterialSupplyHistory>();
    public IReadOnlyCollection<MaterialSupplyHistory> MaterialSupplyHistory => _materialSupplyHistory.AsReadOnly();

    
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
        decimal amount)
    {
        MaterialCategoryId = materialCategoryId;
        Name = name;
        Description = description;
            
        AddSupplyDelivery(DateTime.Now, amount);
    }
    
    public void AddSupplyDelivery(DateTime deliveredDate, decimal amount)
    {
        _materialSupplyHistory.Add(new MaterialSupplyHistory(Id, deliveredDate, amount));
    }

    public decimal GetCurrentAmount()
    {
        // TODO Return real current amount
        return _materialSupplyHistory.FirstOrDefault().Amount;
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
