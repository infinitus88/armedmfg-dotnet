using System;
using System.Runtime.InteropServices.JavaScript;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class ListPagedMaterialTypeRequest : BaseRequest
{
    public int? PageSize { get; set; }
    public int? PageIndex { get; set; }
    public int? MaterialCategoryId { get; set; }

    public ListPagedMaterialTypeRequest(int? pageSize, int? pageIndex, int? materialCategoryId)
    {
        PageSize = pageSize ?? 0;
        PageIndex = pageIndex ?? 0;
        MaterialCategoryId = materialCategoryId;
    }
}

