﻿using System;
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

    public PaymentRecord(DateTime payedDate, int paymentCategoryId, int referenceId, byte paymentMethod, string description, decimal amount)
    {
        PayedDate = payedDate;
        PaymentCategoryId = paymentCategoryId;
        PaymentMethod = (PaymentMethod)paymentMethod;
        Description = description;
        ReferenceId = referenceId;
        Amount = amount;
    }
}

public enum PaymentMethod : byte
{
    TransferWithVAT = 0,
    TransferWithoutVAT = 1,
    CashWithoutVAT = 2,
    CashWithVAT = 3
}

public enum PaymentType : byte
{
    Income = 1,
    Expense = 2,
}
