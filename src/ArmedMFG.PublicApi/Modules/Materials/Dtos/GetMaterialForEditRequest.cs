namespace ArmedMFG.PublicApi.Modules.Materials.Dtos;

public class GetMaterialForEditRequest : BaseRequest
{
    public int MaterialId { get; init; }

    public GetMaterialForEditRequest(int materialId)
    {
        MaterialId = materialId;
    }
}
