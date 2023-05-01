using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.EmployeeEndpoints;

public class SearchEmployeesResponse : BaseResponse
{
    public SearchEmployeesResponse(Guid correlationId) : base(correlationId)
    {
    }

    public SearchEmployeesResponse()
    {
    }

    public List<EmployeeListItemDto> Employees { get; set; } = new List<EmployeeListItemDto>();
    public int TotalCount { get; set; }
}
