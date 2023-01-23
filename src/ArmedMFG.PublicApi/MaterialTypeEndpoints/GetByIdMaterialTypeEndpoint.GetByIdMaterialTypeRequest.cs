namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class GetByIdMaterialTypeRequest : BaseRequest
{
    public int MaterialTypeId { get; init; }

    public GetByIdMaterialTypeRequest(int materialTypeId)
    {
        MaterialTypeId = materialTypeId;
    }
}
