using System;
using ArmedMFG.PublicApi.Modules.Orders.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.Orders.Dtos;

public class GetOrderForEditResponse : BaseResponse
{
    public GetOrderForEditResponse(Guid correlationId) : base(correlationId) { }
    public GetOrderForEditResponse() { }

    public OrderForEditDto OrderForEdit { get; set; }
}
