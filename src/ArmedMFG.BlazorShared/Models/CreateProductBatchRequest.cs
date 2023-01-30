using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ArmedMFG.BlazorShared.Models;

public class CreateProductBatchRequest
{
    [Required(ErrorMessage = "The ProducedDate field is required")]
    public DateTime ProducedDate { get; set; }

    public List<CreateProducedProductRequest> ProducedProducts { get; set; }
    public List<CreateSpentMaterialRequest> SpentMaterials { get; set; }
}
