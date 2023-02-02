namespace ArmedMFG.PublicApi.MaterialTypeEndpoints.MaterialSupplyEndpoints;

public class GetByIdMaterialSupplyRequest : BaseRequest
{
    public int MaterialSupplyId { get; init; }

    public GetByIdMaterialSupplyRequest(int materialSupplyId)
    {
        MaterialSupplyId = materialSupplyId;
    }
}
