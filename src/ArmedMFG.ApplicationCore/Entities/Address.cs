namespace ArmedMFG.ApplicationCore.Entities;

public class Address // ValueObject
{
    public string Region { get; private set; }
    public string District { get; private set; }
    public string Street { get; private set; }

    #pragma warning disable CS8618 // Required by Entity Framework
    public Address() { }

    public Address(string region, string district, string street)
    {
        Region = region;
        District = district;
        Street = street;
    }
}
