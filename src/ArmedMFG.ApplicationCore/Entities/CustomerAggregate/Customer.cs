using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.CustomerAggregate;

public class Customer : BaseEntity, IAggregateRoot
{
    public string FullName { get; private set; }
    public string PhoneNumber { get; private set; }
    public CustomerPosition Position { get; private set; }
    public string? Email { get; private set; }
    public FindOutThrough FindOutThrough { get; private set; }
    public bool IsBusiness { get; private set; }
    public string OrganizationName { get; private set; }
    public string? TaxId { get; private set; }
    public string? Description { get; private set; }

    public Customer(
        string fullName,
        string phoneNumber,
        CustomerPosition position,
        string email,
        bool isBusiness,
        string taxId,
        string organizationName,
        FindOutThrough findOutThrough,
        string description)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        FindOutThrough = findOutThrough;
        Position = position;
        Email = email;
        IsBusiness = isBusiness;
        TaxId = taxId;
        OrganizationName = organizationName;
        Description = description;
    }

    public void UpdateDetails(CustomerDetails details)
    {
        Guard.Against.NullOrEmpty(details.FullName, nameof(details.FullName));
        Guard.Against.NullOrEmpty(details.PhoneNumber, nameof(details.PhoneNumber));

        FullName = details.FullName;
        PhoneNumber = details.PhoneNumber;
        Position = details.Position;
        Email = details.Email;
        IsBusiness = IsBusiness;
        TaxId = TaxId;
        OrganizationName = details.OrganizationName;
        Description = details.Description;
    }
    
    public readonly record struct CustomerDetails
    {
        public string? FullName { get; }
        public string? PhoneNumber { get; }
        public CustomerPosition Position { get; }
        public string? Email { get; }
        public string? TaxId { get; }
        public bool IsBusiness { get; }
        public string? OrganizationName { get; }
        public FindOutThrough? FindOutThrough { get; }
        public string? Description { get; }

        public CustomerDetails(
            string? fullName,
            string? phoneNumber,
            string? email,
            CustomerPosition position,
            bool isBusiness,
            string? taxId,
            string? organizationName,
            FindOutThrough findOutThrough,
            string description)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
            Position = position;
            IsBusiness = isBusiness;
            TaxId = taxId;
            OrganizationName = organizationName;
            FindOutThrough = findOutThrough;
            Description = description;
        }
    }
}

public enum CustomerPosition : byte
{
    Unknown = 0,
    MidleMan = 1,
    Manager = 2,
    Director = 3,
    DeputyDirectory = 4
}

public enum FindOutThrough : byte
{
    Unknown = 0,
    ThroughOlx = 1,
    ThroughSocialNetworks = 2,
    ThroughPartnets = 3,
    ThroughStreetBanners = 4
}
