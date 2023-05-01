﻿namespace ArmedMFG.PublicApi.EmployeeEndpoints;

public class EmployeeListItemDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string DateOfBirth { get; set; }
    public string JoiningDate { get; set; }
    public string PhoneNumber { get; set; }
    public int PositionId { get; set; }
    public string Position { get; set; }
    public byte Status { get; set; }
}
