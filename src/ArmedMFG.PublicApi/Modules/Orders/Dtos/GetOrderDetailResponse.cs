using System;
using ArmedMFG.PublicApi.Modules.Orders.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.Orders.Dtos;

public class GetOrderDetailResponse : BaseResponse
{
    public GetOrderDetailResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetOrderDetailResponse()
    {
    }

    public OrderDetailDto OrderDetail { get; set; }
}
