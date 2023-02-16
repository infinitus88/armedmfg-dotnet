using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;

public class Customer : BaseEntity, IAggregateRoot
{
    public string FullName { get; private set; }
    public string PhoneNumber { get; private set; }
    public int? OrganizationId { get; private set; }
    public CustomerOrganization? CustomerOrganization { get; private set; }
    public string Position { get; private set; }
    public string Email { get; private set; }
    public string FindOutThrough { get; private set; }
    public string? Description { get; private set; }

    public Customer(string fullName, string phoneNumber, string position, string email, string findOutThrough, string description)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        FindOutThrough = findOutThrough;
        Position = position;
        Email = email;
    }

    public void SetOrganization(int organizationId)
    {
        Guard.Against.Negative(organizationId, nameof(organizationId));

        OrganizationId = organizationId;
    }

    public void UpdateDetails(CustomerDetails details)
    {
        Guard.Against.NullOrEmpty(details.FullName, nameof(details.FullName));
        Guard.Against.NullOrEmpty(details.PhoneNumber, nameof(details.PhoneNumber));

        FullName = details.FullName;
        PhoneNumber = details.PhoneNumber;
    }

    public void UpdateOrganization(int organizationId)
    {
        Guard.Against.Zero(organizationId, nameof(organizationId));

        OrganizationId = organizationId;
    }
    
    public readonly record struct CustomerDetails
    {
        public string? FullName { get; }
        public string? PhoneNumber { get; }
        public string? Position { get; }
        public string? Email { get; }
        public string? FindOutThrough { get; }

        public CustomerDetails(string? fullName, string? phoneNumber, string email, string position, string? findOutThrough)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
            Position = position;
            FindOutThrough = findOutThrough;
        }
    }
}
