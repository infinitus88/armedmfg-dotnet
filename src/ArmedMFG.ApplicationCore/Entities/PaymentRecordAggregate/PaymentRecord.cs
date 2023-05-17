using System;
using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;

public class PaymentRecord : BaseEntity, IAggregateRoot
{
    public DateTime PayedDate { get; private set; }
    public PaymentCategory? PaymentCategory { get; private set; }
    public int PaymentCategoryId { get; private set; }
    public int ReferenceId { get; private set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; private set; }

    public PaymentRecord(DateTime payedDate, int paymentCategoryId, int referenceId, PaymentMethod paymentMethod, string description, decimal amount)
    {
        PayedDate = payedDate;
        PaymentCategoryId = paymentCategoryId;
        PaymentMethod = paymentMethod;
        Description = description;
        ReferenceId = referenceId;
        Amount = amount;
    }

    public void UpdateDetails(PaymentRecordDetails details)
    {
        Guard.Against.Default(details.PayedDate, nameof(details.PayedDate));
        Guard.Against.Zero(details.ReferenceId, nameof(details.ReferenceId));
        Guard.Against.Negative(details.Amount, nameof(details.Amount));

        PayedDate = details.PayedDate;
        ReferenceId = details.ReferenceId;
        Amount = details.Amount;
        Description = details.Description;
    }

    public readonly record struct PaymentRecordDetails
    {
        public DateTime PayedDate { get; init; }
        public int ReferenceId { get; init; }
        public PaymentMethod PaymentMethod { get; init; }
        public decimal Amount { get; init; }
        public string? Description { get; init; }

        public PaymentRecordDetails(DateTime payedDate, int referenceId, PaymentMethod paymentMethod, decimal amount, string? description) : this()
        {
            PayedDate = payedDate;
            ReferenceId = referenceId;
            PaymentMethod = paymentMethod;
            Amount = amount;
            Description = description;
        }
    }
}

public enum PaymentMethod : byte
{
    TransferWithVat = 0,
    TransferWithoutVat = 1,
    CashWithoutVat = 2,
    CashWithVat = 3
}

public enum PaymentType : byte
{
    Income = 1,
    Expense = 2,
}
