using System;
using ArmedMFG.PublicApi.Modules.Products.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.Products.Dtos;

public class GetProductForEditResponse : BaseResponse
{
    public GetProductForEditResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetProductForEditResponse()
    {
    }

    public ProductForEditDto ProductForEdit { get; set; }
}
