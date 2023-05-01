using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Interfaces;
using static ArmedMFG.ApplicationCore.Entities.OrderAggregate.Order;

namespace ArmedMFG.ApplicationCore.Entities.EmployeeAggregate;

public class Employee : BaseEntity, IAggregateRoot
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime JoiningDate { get; set; }
    public int PositionId { get; set; }
    public EmployeePosition? Position { get; set; }
    public EmployeeStatus Status { get; set; }

    public Employee(string fullName, string phoneNumber, DateTime dateOfBirth, DateTime joiningDate, int positionId, EmployeeStatus status = EmployeeStatus.Active)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
        JoiningDate = joiningDate;
        PositionId = positionId;
        Status = status;
    }

    public void SetStatus(byte status)
    {
        Status = (EmployeeStatus)status;
    }

    public void UpdateDetails(EmployeeDetails details)
    {
        Guard.Against.NullOrWhiteSpace(details.FullName, nameof(details.FullName));
        Guard.Against.NullOrWhiteSpace(details.PhoneNumber, nameof(details.PhoneNumber));
        Guard.Against.Null(details.Status, nameof(details.Status));
        Guard.Against.Default(details.DateOfBirth, nameof(details.DateOfBirth));
        Guard.Against.Default(details.JoiningDate, nameof(details.JoiningDate));

        Guard.Against.Zero(details.PositionId, nameof(details.PositionId));

        FullName = details.FullName;
        PhoneNumber = details.PhoneNumber;
        SetStatus(details.Status);

        DateOfBirth = details.DateOfBirth;
        PositionId = details.PositionId;
    }

    public readonly record struct EmployeeDetails
    {
        public string FullName { get; }
        public string PhoneNumber { get; }
        public DateTime DateOfBirth { get; }
        public DateTime JoiningDate { get; }
        public int PositionId { get; }
        public byte Status { get; }

        public EmployeeDetails(int positionId) : this()
        {
            PositionId = positionId;
        }

        public EmployeeDetails(string fullName, string phoneNumber, DateTime dateOfBirth, DateTime joiningDate, int positionId, byte status)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            JoiningDate = joiningDate;
            PositionId = positionId;
            Status = status;
        }
    }
}

public enum EmployeeStatus : byte
{
    Active = 0,
    Fired = 1,
    OnVacation = 2,
}
