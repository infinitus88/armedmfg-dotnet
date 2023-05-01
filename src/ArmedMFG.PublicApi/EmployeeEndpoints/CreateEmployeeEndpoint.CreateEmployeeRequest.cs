using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.EmployeeEndpoints;

public class CreateEmployeeRequest : BaseRequest
{
    public string FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public int PositionId { get; set; }
    public string DateOfBirth { get; set; }
    public string JoiningDate { get; set; }
    public byte Status { get; set; }
}
