using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.EmployeeEndpoints;

public class GetEmployeeOptionsResponse : BaseResponse
{
    public GetEmployeeOptionsResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetEmployeeOptionsResponse()
    {
    }

    public List<EmployeeOptionDto> Employees { get; set; } = new List<EmployeeOptionDto>();
}
