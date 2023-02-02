using System;
using ArmedMFG.ApplicationCore.Entities;

namespace ArmedMFG.PublicApi.ClientEndpoints;

public class ClientDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public int? OrganizationId { get; set; }
    public string? Email { get; set; }
}
