namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class MaterialTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal CurrentAmount { get; set; }
    public int MaterialCategoryId { get; set; }
}
