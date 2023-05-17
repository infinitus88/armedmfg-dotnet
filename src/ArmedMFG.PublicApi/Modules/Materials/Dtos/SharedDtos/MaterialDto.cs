namespace ArmedMFG.PublicApi.Modules.Materials.Dtos.SharedDtos;

public class MaterialDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MaterialCategoryId { get; set; }
    public double Amount { get; set; }
}
