using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.AttendanceEndpoints;

public class CreateAttendanceRecordRequest : BaseRequest
{
    public string? Date { get; set; }
    public List<AttendanceRecordDto> AttendanceRecords { get; set; } = new List<AttendanceRecordDto>();
}
