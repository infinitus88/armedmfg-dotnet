using System;

namespace ArmedMFG.PublicApi.ClientEndpoints;

public class CreateClientRequest : BaseRequest
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string FindOutThrough { get; set; }
    public int? OrganizationId { get; set; }
}
