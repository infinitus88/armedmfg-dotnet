using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.ClientAggregate;

public class Organization : BaseEntity, IAggregateRoot
{
    public string Name { get; set; }
    public string? TIN { get; set; }
    public Address? MainBranchAddress { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Description { get; set; }


    public Organization(string name, string tin, Address mainBranchAddress, string phoneNumber)
    {
        Name = name;
        TIN = tin;
        MainBranchAddress = mainBranchAddress;
        PhoneNumber = phoneNumber;
    }

    public void UpdateDetails(OrganizationDetails details)
    {
        Guard.Against.NullOrEmpty(details.Name, nameof(details.Name));
        Guard.Against.NullOrEmpty(details.TIN, nameof(details.TIN));
        Guard.Against.NullOrEmpty(details.PhoneNumber, nameof(details.PhoneNumber));
        Guard.Against.Null(details.MainBranchAddress, nameof(details.MainBranchAddress));

        Name = details.Name;
        TIN = details.TIN;
        PhoneNumber = details.PhoneNumber;
        MainBranchAddress = details.MainBranchAddress;
    }

    public readonly record struct OrganizationDetails
    {
        public OrganizationDetails(string? name, string? tin, string? phoneNumber, Address? mainBranchAddress)
        {
            Name = name;
            TIN = tin;
            PhoneNumber = phoneNumber;
            MainBranchAddress = mainBranchAddress;
        }

        public string? Name { get; }
        public string? TIN { get; }
        public string? PhoneNumber { get; }
        public Address? MainBranchAddress { get; }
    }
}
