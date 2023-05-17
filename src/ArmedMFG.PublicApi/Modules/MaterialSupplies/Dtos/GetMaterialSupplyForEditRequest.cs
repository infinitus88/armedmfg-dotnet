namespace ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos;

public class GetMaterialSupplyForEditRequest : BaseRequest
{
    public int MaterialSupplyId { get; init; }

    public GetMaterialSupplyForEditRequest(int materialSupplyId)
    {
        MaterialSupplyId = materialSupplyId;
    }
}
