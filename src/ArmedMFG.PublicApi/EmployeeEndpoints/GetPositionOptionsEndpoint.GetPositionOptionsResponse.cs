using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.EmployeeEndpoints;

public class GetPositionOptionsResponse : BaseResponse
{
    public GetPositionOptionsResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetPositionOptionsResponse()
    {
    }

    public List<PositionOptionDto> Positions { get; set; } = new List<PositionOptionDto>();
}
