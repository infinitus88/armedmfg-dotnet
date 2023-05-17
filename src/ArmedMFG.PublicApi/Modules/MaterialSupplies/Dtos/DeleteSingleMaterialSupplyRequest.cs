namespace ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos;

public class DeleteSingleMaterialSupplyRequest : BaseRequest
{
    public int MaterialSupplyId { get; set; }

    public DeleteSingleMaterialSupplyRequest(int materialSupplyId)
    {
        MaterialSupplyId = materialSupplyId;
    }
}
