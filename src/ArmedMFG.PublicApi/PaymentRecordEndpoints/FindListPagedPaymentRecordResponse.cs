using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.PaymentRecordEndpoints;

public class FindListPagedPaymentRecordResponse : BaseResponse
{
    public FindListPagedPaymentRecordResponse(Guid correlationId) : base(correlationId)
    {
    }

    public FindListPagedPaymentRecordResponse()
    {
    }

    public List<PaymentRecordDto> PaymentRecords { get; set; } = new List<PaymentRecordDto>();
    public int TotalCount { get; set; }
}
