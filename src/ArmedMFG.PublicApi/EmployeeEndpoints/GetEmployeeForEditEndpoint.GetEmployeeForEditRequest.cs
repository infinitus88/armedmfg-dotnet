namespace ArmedMFG.PublicApi.EmployeeEndpoints;

public class GetEmployeeForEditRequest : BaseRequest
{
    public int EmployeeId { get; init; }

    public GetEmployeeForEditRequest(int employeeId)
    {
        EmployeeId = employeeId;
    }
}
