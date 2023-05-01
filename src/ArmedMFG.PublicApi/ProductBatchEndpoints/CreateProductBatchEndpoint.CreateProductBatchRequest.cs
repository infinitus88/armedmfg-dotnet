using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class CreateProductBatchRequest : BaseRequest
{
    public string ProducedDate { get; set; }
    public IList<CreateProducedProductRequest> ProducedProducts { get; set; }
    public IList<CreateSpentMaterialRequest> SpentMaterials { get; set; }
}
