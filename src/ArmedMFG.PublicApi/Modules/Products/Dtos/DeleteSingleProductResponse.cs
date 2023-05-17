using System;

namespace ArmedMFG.PublicApi.Modules.Products.Dtos;

public class DeleteSingleProductResponse : BaseResponse
{
    public DeleteSingleProductResponse(Guid correlationId)
        : base()
    {
    }

    public DeleteSingleProductResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}
