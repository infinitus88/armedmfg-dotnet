using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Entities.ProductAggregate;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.MaterialAggregate;

public class Material : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public int MaterialCategoryId { get; private set; }
    public MaterialCategory? MaterialCategory { get; private set; }
    public Unit Unit { get; private set; }
    public double Amount { get; private set; }

    private readonly List<MaterialAmountHistory> _materialAmountHistory = new List<MaterialAmountHistory>();
    public IReadOnlyCollection<MaterialAmountHistory> MaterialAmountHistory => _materialAmountHistory;
    
    private readonly List<MaterialSupply> _materialSupplies = new List<MaterialSupply>();
    public IReadOnlyCollection<MaterialSupply> MaterialSupplies => _materialSupplies.AsReadOnly();

    
    public Material(int materialCategoryId,
        string name,
        Unit unit,
        double amount)
    {
        MaterialCategoryId = materialCategoryId;
        Name = name;
        Unit = unit;
        Amount = amount;
    }

    
    public void AddSupplyDelivery(DateTime deliveredDate, decimal price, double amount)
    {
        _materialSupplies.Add(new MaterialSupply(Id, deliveredDate, price, amount));
    }

    public void AddAmountChangeRecord(DateTime changeDate, int materialId, double fromAmount, double toAmount, byte changeType)
    {
        _materialAmountHistory.Add(new MaterialAmountHistory(changeDate, materialId, fromAmount, toAmount, 
            (MaterialAmountChangeType)changeType));
    }

    public void UpdateDetails(MaterialDetails details)
    {
        Guard.Against.NullOrEmpty(details.Name, nameof(details.Name));

        Name = details.Name;
        Unit = details.Unit;
        UpdateCategory(details.MaterialCategoryId);
    }

    public void UpdateCategory(int materialCategoryId)
    {
        Guard.Against.Zero(materialCategoryId, nameof(materialCategoryId));

        MaterialCategoryId = materialCategoryId;
    }

    public readonly record struct MaterialDetails
    {
        public string? Name { get; }
        public Unit Unit { get; }
        public int MaterialCategoryId { get; }

        public MaterialDetails(string? name, Unit unit, int materialCategoryId)
        {
            Name = name;
            Unit = unit;
            MaterialCategoryId = materialCategoryId;
        }
    }
}

public enum Unit : byte
{
    Meter = 0,
    Kilogram = 1,
    Liter = 2
}
