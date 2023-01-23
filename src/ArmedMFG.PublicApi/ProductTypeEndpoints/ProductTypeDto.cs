using System;
using System.Collections.Generic;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.CatalogItemEndpoints;
using Microsoft.AspNetCore.Http;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class ProductTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    // public decimal CurrentPrice { get; set; }
    public string PictureUri { get; set; }
    public int ProductCategoryId { get; set; }
}

public class ListPagedProductTypeEndpoint : IEndpoint<IResult, ListPagedProductTypeRequest, IRepository<ProductType>>
{
    
}

public class ListPagedProductTypeRequest : BaseRequest
{
    public int? PageSize { get; set; }
    public int? PageIndex { get; set; }
    public int? ProductCategoryId { get; set; }

    public ListPagedProductTypeRequest(int? pageSize, int? pageIndex, int? productCategoryId)
    {
        PageSize = pageSize ?? 0;
        PageIndex = pageIndex ?? 0;
        ProductCategoryId = productCategoryId;
    }
}

public class ListPagedProductTypeResponse : BaseResponse
{
    public ListPagedProductTypeResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListPagedProductTypeResponse()
    {
    }

    public List<ProductTypeDto> ProductTypes { get; set; } = new List<ProductTypeDto>();
    public int PageCount {get; set; }
}
