using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;

public class CustomerOrganization : BaseEntity, IAggregateRoot
{
    public string Name { get; set; }
    public string? TIN { get; set; }
    public Address? MainBranchAddress { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Description { get; set; }


    public CustomerOrganization(string name, string tin, Address mainBranchAddress, string phoneNumber, string description)
    {
        Name = name;
        TIN = tin;
        MainBranchAddress = mainBranchAddress;
        PhoneNumber = phoneNumber;
        Description = description;
    }

    public void UpdateDetails(OrganizationDetails details)
    {
        Guard.Against.NullOrEmpty(details.Name, nameof(details.Name));
        Guard.Against.NullOrEmpty(details.TIN, nameof(details.TIN));
        Guard.Against.NullOrEmpty(details.PhoneNumber, nameof(details.PhoneNumber));
        Guard.Against.NullOrEmpty(details.Description, nameof(details.Description));
        Guard.Against.Null(details.MainBranchAddress, nameof(details.MainBranchAddress));

        Name = details.Name;
        TIN = details.TIN;
        PhoneNumber = details.PhoneNumber;
        MainBranchAddress = details.MainBranchAddress;
        Description = details.Description;
    }

    public readonly record struct OrganizationDetails
    {
        public OrganizationDetails(string? name, string? tin, string? phoneNumber, Address? mainBranchAddress, string? description)
        {
            Name = name;
            TIN = tin;
            PhoneNumber = phoneNumber;
            MainBranchAddress = mainBranchAddress;
            Description = description;
        }

        public string? Name { get; }
        public string? TIN { get; }
        public string? PhoneNumber { get; }
        public Address? MainBranchAddress { get; }
        public string? Description { get; }
    }
}
