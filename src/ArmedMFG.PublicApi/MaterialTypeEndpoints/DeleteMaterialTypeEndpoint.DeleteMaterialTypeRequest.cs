namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class DeleteMaterialTypeRequest : BaseRequest
{
    public int MaterialTypeId { get; set; }

    public DeleteMaterialTypeRequest(int materialTypeId)
    {
        MaterialTypeId = materialTypeId;
    }
}
