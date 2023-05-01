using System;

namespace ArmedMFG.PublicApi.EmployeeEndpoints;

public class GetEmployeeForEditResponse : BaseResponse
{
    public GetEmployeeForEditResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetEmployeeForEditResponse()
    {
    }

    public EmployeeForEditDto Employee { get; set; }
}
