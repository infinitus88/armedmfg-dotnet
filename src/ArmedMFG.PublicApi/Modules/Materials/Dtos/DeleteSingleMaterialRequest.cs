namespace ArmedMFG.PublicApi.Modules.Materials.Dtos;

public class DeleteSingleMaterialRequest : BaseRequest
{
    public int MaterialId { get; set; }

    public DeleteSingleMaterialRequest(int materialId)
    {
        MaterialId = materialId;
    }
}
