using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using ArmedMFG.ApplicationCore.Entities.EmployeeAggregate;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Exceptions;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.AttendanceEndpoints;

public class CreateAttendanceRecordEndpoint : IEndpoint<IResult, CreateAttendanceRecordRequest, IRepository<EmployeeAttendance>>
{
    private readonly IMapper _mapper;
    private readonly DateParsingSettings _dateParsingSettings;

    public CreateAttendanceRecordEndpoint(IMapper mapper, IOptions<DateParsingSettings> dateParsingSettings)
    {
        _mapper = mapper;
        _dateParsingSettings = dateParsingSettings.Value;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/employees/attendance",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS,
                    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
                async
                    (CreateAttendanceRecordRequest request, IRepository<EmployeeAttendance> attendanceRepository) =>
                {
                    return await HandleAsync(request, attendanceRepository);
                })
            .Produces<CreateAttendanceRecordResponse>()
            .WithTags("EmployeeEndpoints");
    }
    
    public async Task<IResult> HandleAsync(CreateAttendanceRecordRequest request,
        IRepository<EmployeeAttendance> attendanceRepository)
    {
        var response = new CreateAttendanceRecordResponse(request.CorrelationId());
        
        // var productPriceNameSpecification = new ProductPrice
        
        foreach (var attendanceRecord in request.AttendanceRecords)
        {
            await attendanceRepository.AddAsync(
                new(DateTime.ParseExact(request.Date, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
                    attendanceRecord.EmployeeId, attendanceRecord.IsPresent));
        }

        return Results.Ok();
    }
}
