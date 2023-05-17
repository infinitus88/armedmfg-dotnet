namespace ArmedMFG.PublicApi.Modules.Materials.Dtos;

public class CreateMaterialRequest : BaseRequest
{
    public int MaterialCategoryId { get; set; }
    public string? Name { get; set; }
    public byte Unit { get; set; }
    public double Amount { get; set; }
}
