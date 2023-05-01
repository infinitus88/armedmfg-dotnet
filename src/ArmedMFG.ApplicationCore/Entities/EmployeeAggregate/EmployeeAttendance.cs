using System;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.EmployeeAggregate;

public class EmployeeAttendance : BaseEntity, IAggregateRoot
{
    public DateTime Date { get; set; }
    public Calendar Calendar { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public bool IsPresent { get; set; }

    public EmployeeAttendance(DateTime date, int employeeId, bool isPresent)
    {
        Date = date;
        EmployeeId = employeeId;
        IsPresent = isPresent;
    }
}
