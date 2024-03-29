﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class UpdateProductBatchRequest : BaseRequest
{
    [Range(1, 10000)]
    public int Id { get; set; }
    
    [Required]
    public DateTime ProducedDate { get; set; }
    
    [Required]
    [JsonPropertyName("spentMaterials")]
    public List<UpdateSpentMaterialRequest> SpentMaterials { get; set; }
    
    [Required]
    [JsonPropertyName("producedProducts")]
    public List<UpdateProducedProductRequest> ProducedProducts { get; set; }
}
