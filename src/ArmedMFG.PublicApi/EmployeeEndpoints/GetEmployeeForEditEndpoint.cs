using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.EmployeeAggregate;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using ArmedMFG.PublicApi.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.EmployeeEndpoints;

public class GetEmployeeForEditEndpoint : IEndpoint<IResult, GetEmployeeForEditRequest, IRepository<Employee>>
{
    private readonly IMapper _mapper;
    private readonly DateParsingSettings _dateParsingSettings;

    public GetEmployeeForEditEndpoint(IMapper mapper, IOptions<DateParsingSettings> dateParsingSettings)
    {
        _mapper = mapper;
        _dateParsingSettings = dateParsingSettings.Value;
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/employees/for_edit/{employeeId}",
                async (int employeeId, IRepository<Employee> employeeRepository) =>
                {
                    return await HandleAsync(new GetEmployeeForEditRequest(employeeId), employeeRepository);
                })
            .Produces<GetEmployeeForEditResponse>()
            .WithTags("EmployeeEndpoints");
    }
    
    public async Task<IResult> HandleAsync(GetEmployeeForEditRequest request, IRepository<Employee> employeeForRepository)
    {
        var response = new GetEmployeeForEditResponse(request.CorrelationId());

        var filterSpec = new EmployeeForEditSpecification(request.EmployeeId);

        var employee = await employeeForRepository.GetBySpecAsync(filterSpec);
        
        if (employee is null)
            return Results.NotFound();

        response.Employee = _mapper.Map<EmployeeForEditDto>(employee);

        return Results.Ok(response);
    }
}
