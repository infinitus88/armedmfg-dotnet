using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class CreateProductBatchRequest
{
    [Required(ErrorMessage = "The ProducedDate field is required")]
    public DateTime ProducedDate { get; set; }
    
    public List<ProducedProduct> ProducedProducts { get; set; }
    public List<SpentMaterial> SpentMaterials { get; set; }
}
