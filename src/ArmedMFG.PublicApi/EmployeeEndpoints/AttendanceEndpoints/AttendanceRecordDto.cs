namespace ArmedMFG.PublicApi.AttendanceEndpoints;

public class AttendanceRecordDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public bool IsPresent { get; set; }
}
