using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.StatisticsEndpoints;

public class PaymentRecordsStatisticsResponse : BaseResponse
{
    public PaymentRecordsStatisticsResponse(Guid correlationId) : base(correlationId)
    {
    }

    public PaymentRecordsStatisticsResponse()
    {
    }

    public List<PaymentRecordCategoryInfoDto> IncomeCategoriesInfo { get; set; } = new List<PaymentRecordCategoryInfoDto>();
    public List<PaymentRecordCategoryInfoDto> ExpenseCategoriesInfo { get; set; } = new List<PaymentRecordCategoryInfoDto>();
    public decimal IncomeAmount { get; set; }
    public decimal ExpenseAmount { get; set; }
}

public class PaymentRecordCategoryInfoDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal TotalAmount { get; set; }

    public PaymentRecordCategoryInfoDto(int id, string name, decimal totalAmount)
    {
        Id = id;
        Name = name;
        TotalAmount = totalAmount;
    }
}
