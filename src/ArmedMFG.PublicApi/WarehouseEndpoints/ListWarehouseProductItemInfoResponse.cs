using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.WarehouseEndpoints;

public class ListWarehouseProductItemInfoResponse : BaseResponse
{
    public ListWarehouseProductItemInfoResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListWarehouseProductItemInfoResponse()
    {
    }

    public List<WarehouseItemInfoDto> WarehouseItemInfo { get; set; } = new List<WarehouseItemInfoDto>();
}
