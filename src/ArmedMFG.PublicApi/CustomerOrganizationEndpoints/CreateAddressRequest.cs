using System;

namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class CreateAddressRequest
{
    public string Region { get; set; }
    public string District { get; set; }
    public string Street { get; set; }
}
