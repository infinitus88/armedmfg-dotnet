using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.PaymentRecordEndpoints;

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
