using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.Modules.Materials.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.Materials.Dtos;

public class SearchMaterialResponse : BaseResponse
{
    public SearchMaterialResponse(Guid correlationId) : base(correlationId)
    {
    }

    public SearchMaterialResponse()
    {
    }

    public List<MaterialDto> Materials { get; set; } = new List<MaterialDto>();
    public int TotalCount { get; set; }
}
