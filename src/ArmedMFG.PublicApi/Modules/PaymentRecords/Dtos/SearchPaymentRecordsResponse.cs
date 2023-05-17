using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.Modules.Customers.Dtos.Shared;
using ArmedMFG.PublicApi.Modules.PaymentRecords.Dtos.Shared;

namespace ArmedMFG.PublicApi.Modules.PaymentRecords.Dtos;

public class SearchPaymentRecordsResponse : BaseResponse
{
    public SearchPaymentRecordsResponse(Guid correlationId) : base(correlationId)
    {
    }

    public SearchPaymentRecordsResponse()
    {
    }

    public List<PaymentRecordDto> PaymentRecords { get; set; } = new List<PaymentRecordDto>();
    public decimal IncomeAmount { get; set; }
    public decimal ExpenseAmount { get; set; }
    public int TotalCount { get; set; }
}
