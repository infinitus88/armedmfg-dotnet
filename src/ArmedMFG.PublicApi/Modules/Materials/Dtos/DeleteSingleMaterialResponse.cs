using System;

namespace ArmedMFG.PublicApi.Modules.Materials.Dtos;

public class DeleteSingleMaterialResponse : BaseResponse
{
    public DeleteSingleMaterialResponse(Guid correlationId)
        : base()
    {
    }

    public DeleteSingleMaterialResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}
