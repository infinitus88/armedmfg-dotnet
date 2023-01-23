using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class UpdateMaterialTypeRequest : BaseRequest
{
    [Range(1, 10000)]
    public int Id { get; set; }
    [Range(1, 10000)]
    public int MaterialCategoryId { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Name { get; set; }
}
