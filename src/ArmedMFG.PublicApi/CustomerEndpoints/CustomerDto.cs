﻿namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class CustomerDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public int? OrganizationId { get; set; }
    public string? Email { get; set; }
    public string? Position { get; set; }
    public string? FindOutThrough { get; set; }
}
