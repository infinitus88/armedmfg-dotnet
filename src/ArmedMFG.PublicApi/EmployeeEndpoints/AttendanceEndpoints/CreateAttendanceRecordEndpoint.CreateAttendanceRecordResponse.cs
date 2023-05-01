using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.AttendanceEndpoints;

public class CreateAttendanceRecordResponse : BaseResponse
{
    public CreateAttendanceRecordResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateAttendanceRecordResponse()
    {
    }

    public List<AttendanceRecordDto> AttendanceRecords { get; set; }
}
