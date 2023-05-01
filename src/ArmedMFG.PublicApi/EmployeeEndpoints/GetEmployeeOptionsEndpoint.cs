using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.EmployeeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.EmployeeEndpoints;

public class GetEmployeeOptionsEndpoint : IEndpoint<IResult, IRepository<Employee>>
{
    private readonly IMapper _mapper;

    public GetEmployeeOptionsEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/employees/input-options",
                async (IRepository<Employee> employeeRepository) =>
                {
                    return await HandleAsync(employeeRepository);
                })
            .Produces<GetEmployeeOptionsResponse>()
            .WithTags("EmployeesEndpoints");
    }

    public async Task<IResult> HandleAsync(IRepository<Employee> employeeRepository)
    {
        // await Task.Delay(1000);
        var response = new GetEmployeeOptionsResponse();

        var employees = await employeeRepository.ListAsync();

        foreach (var employee in employees)
        {
            response.Employees.Add(new EmployeeOptionDto() { Id = employee.Id, FullName = employee.FullName });
        }

        return Results.Ok(response);
    }
}
