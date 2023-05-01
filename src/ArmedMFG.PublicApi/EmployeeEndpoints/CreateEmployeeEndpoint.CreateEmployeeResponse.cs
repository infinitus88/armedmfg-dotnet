using System;

namespace ArmedMFG.PublicApi.EmployeeEndpoints;

public class CreateEmployeeResponse : BaseResponse
{
    public CreateEmployeeResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateEmployeeResponse()
    {
    }

    public EmployeeListItemDto Employee { get; set; }
}
