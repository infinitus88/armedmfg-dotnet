namespace ArmedMFG.PublicApi.Modules.Materials.Dtos.SharedDtos;

public class MaterialForEditDto
{
    public int Id { get; set; }
    public int MaterialCategoryId { get; set; }
    public string Name { get; set; }
    public byte Unit { get; set; }
}
