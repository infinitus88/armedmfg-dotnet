namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class CreateMaterialTypeRequest : BaseRequest
{
    public int MaterialCategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal CurrentAmount { get; set; }
}
