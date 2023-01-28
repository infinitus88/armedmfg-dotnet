namespace ArmedMFG.PublicApi.MaterialTypeEndpoints.MaterialSupplyEndpoints;

public class DeleteMaterialSupplyRequest : BaseRequest
{
    public int MaterialSupplyId { get; set; }

    public DeleteMaterialSupplyRequest(int materialSupplyId)
    {
        MaterialSupplyId = materialSupplyId;
    }
}
