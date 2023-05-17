using System;

namespace ArmedMFG.PublicApi.Modules.ProductMaterials.Dtos;

public class DeleteSingleProductMaterialResponse : BaseResponse
{
    public DeleteSingleProductMaterialResponse(Guid correlationId) : base(correlationId)
    {
    }

    public DeleteSingleProductMaterialResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}
