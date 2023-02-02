using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.ClientAggregate;

public class Client : BaseEntity, IAggregateRoot
{
    public string FullName { get; private set; }
    public string PhoneNumber { get; private set; }
    public int? OrganizationId { get; private set; }
    public Organization? Organization { get; private set; }
    public string? Email { get; private set; }
    public string FindOutThrough { get; private set; }

    public Client(string fullName, string phoneNumber, string findOutThrough)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        FindOutThrough = findOutThrough;
    }

    public void SetOrganization(int organizationId)
    {
        Guard.Against.Zero(organizationId, nameof(organizationId));

        OrganizationId = organizationId;
    }

    public void UpdateDetails(ClientDetails details)
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
    
    public readonly record struct ClientDetails
    {
        public string? FullName { get; }
        public string? PhoneNumber { get; }
        public string? Email { get; }
        public string? FindOutThrough { get; }

        public ClientDetails(string? fullName, string? phoneNumber, string email, string? findOutThrough)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
            FindOutThrough = findOutThrough;
        }
    }
}
