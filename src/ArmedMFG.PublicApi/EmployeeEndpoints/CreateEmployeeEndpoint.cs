using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
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

namespace ArmedMFG.PublicApi.EmployeeEndpoints;

public class CreateOrderEndpoint : IEndpoint<IResult, CreateEmployeeRequest, IRepository<Employee>, IRepository<EmployeePosition>>
{
    private readonly IMapper _mapper;
    private readonly DateParsingSettings _dateParsingSettings;

    public CreateOrderEndpoint(IMapper mapper, IOptions<DateParsingSettings> dateParsingSettings)
    {
        _mapper = mapper;
        _dateParsingSettings = dateParsingSettings.Value;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/employees",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS,
                    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
                async
                    (CreateEmployeeRequest request, IRepository<Employee> employeeRepository, IRepository<EmployeePosition> positionRepository) =>
                {
                    return await HandleAsync(request, employeeRepository, positionRepository);
                })
            .Produces<CreateEmployeeResponse>()
            .WithTags("EmployeeEndpoints");
    }
    
    public async Task<IResult> HandleAsync(CreateEmployeeRequest request,
        IRepository<Employee> employeeRepository, IRepository<EmployeePosition> positionRepository)
    {
        var response = new CreateEmployeeResponse(request.CorrelationId());
        
        // var productPriceNameSpecification = new ProductPrice
        
        var existingPosition = await positionRepository.GetByIdAsync(request.PositionId);
        
        if (existingPosition == null)
        {
            throw new NotFoundException($"A employee's position with Id: {request.PositionId} is not found");
        }

        var newEmployee = new Employee(request.FullName, request.PhoneNumber,
            DateTime.ParseExact(request.DateOfBirth, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact(request.JoiningDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            request.PositionId);

        newEmployee.SetStatus(request.Status);
        
        newEmployee = await employeeRepository.AddAsync(newEmployee);

        response.Employee = _mapper.Map<EmployeeListItemDto>(newEmployee);
        return Results.Created($"api/employees/{newEmployee.Id}", response);
    }
}
