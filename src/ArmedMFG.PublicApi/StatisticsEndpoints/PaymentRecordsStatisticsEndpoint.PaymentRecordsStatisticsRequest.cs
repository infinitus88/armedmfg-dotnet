using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.StatisticsEndpoints;

public class PaymentRecordsStatisticsRequest : BaseRequest
{
    public string StartDate { get; set; }
    public string EndDate { get; set; }

    public PaymentRecordsStatisticsRequest()
    {
    }
}

