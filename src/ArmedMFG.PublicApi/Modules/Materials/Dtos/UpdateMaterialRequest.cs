using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.Modules.Materials.Dtos;

public class UpdateMaterialRequest : BaseRequest
{
    [Range(1, 10000)]
    public int Id { get; set; }

    [Range(1, 10000)]
    public int MaterialCategoryId { get; set; }

    [Range(1, 10000)]
    public byte Unit { get; set; }

    [Required]
    public string Name { get; set; }
}
