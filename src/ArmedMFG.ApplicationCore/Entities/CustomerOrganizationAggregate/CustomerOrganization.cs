using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;

public class CustomerOrganization : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string? TaxpayerIdNum { get; private set; }
    public Address? MainBranchAddress { get; private set; }
    public string? Email { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? Description { get; private set; }


    public CustomerOrganization(string name, string taxpayerIdNum, string phoneNumber, string email, string description)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        TaxpayerIdNum = taxpayerIdNum;
        Description = description;
    }

    public void SetAddress(string region, string district, string street)
    {
        MainBranchAddress = new Address(region, district, street);
    }

    public void UpdateDetails(OrganizationDetails details)
    {
        Guard.Against.NullOrEmpty(details.Name, nameof(details.Name));
        Guard.Against.NullOrEmpty(details.TaxpayerIdNum, nameof(details.TaxpayerIdNum));
        Guard.Against.NullOrEmpty(details.PhoneNumber, nameof(details.PhoneNumber));
        Guard.Against.NullOrEmpty(details.Email, nameof(details.Email));
        Guard.Against.NullOrEmpty(details.Description, nameof(details.Description));

        Name = details.Name;
        TaxpayerIdNum = details.TaxpayerIdNum;
        PhoneNumber = details.PhoneNumber;
        Email = details.Email;
        Description = details.Description;
    }

    public readonly record struct OrganizationDetails
    {
        public OrganizationDetails(string? name, string? taxpayerIdNum, string? phoneNumber, string? email, string? description)
        {
            Name = name;
            TaxpayerIdNum = taxpayerIdNum;
            PhoneNumber = phoneNumber;
            Email = email;
            Description = description;
        }

        public string? Name { get; }
        public string? TaxpayerIdNum { get; }
        public string? PhoneNumber { get; }
        public string? Email { get; }
        public string? Description { get; }
    }
}
