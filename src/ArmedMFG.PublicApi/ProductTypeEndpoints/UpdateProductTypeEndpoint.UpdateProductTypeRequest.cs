using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class UpdateProductTypeRequest : BaseRequest
{
    [Range(1, 10000)]
    public int Id { get; set; }
    [Range(1, 10000)]
    public int ProductCategoryId { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Name { get; set; }
    public string PictureBase64 { get; set; }
    public string PictureUri { get; set; }
    public string PictureName { get; set; }
}
