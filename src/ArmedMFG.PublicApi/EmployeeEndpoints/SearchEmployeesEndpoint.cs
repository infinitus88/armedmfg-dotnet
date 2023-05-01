using System;
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
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.EmployeeEndpoints;

public class SearchEmployeesEndpoint : IEndpoint<IResult, SearchEmployeesRequest, IRepository<Employee>>
{
    private readonly IMapper _mapper;

    public SearchEmployeesEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/employees/search",
                async (SearchEmployeesRequest request, IRepository<Employee> employeeRepository) =>
                {
                    return await HandleAsync(request, employeeRepository);
                })
            .Produces<SearchEmployeesResponse>()
            .WithTags("EmployeeEndpoints");
    }
    
    public async Task<IResult> HandleAsync(SearchEmployeesRequest request, IRepository<Employee> employeeRepository)
    {
        //await Task.Delay(1000);
        var response = new SearchEmployeesResponse(request.CorrelationId());

        var filterSpec = new EmployeeFilterSpecification(request.Filter.SearchText, request.Filter.PositionId, request.Filter.Status);
        int totalItems = await employeeRepository.CountAsync(filterSpec);

        var pagedSpec = new EmployeeFilterPaginatedSpecification(
            skip: (request.PageNumber.Value - 1) * request.PageSize.Value,
            take: request.PageSize.Value,
            request.Filter.SearchText,
            request.Filter.PositionId,
            request.Filter.Status);

        var orders = await employeeRepository.ListAsync(pagedSpec);

        response.Employees.AddRange(orders.Select(((IMapperBase)_mapper).Map<EmployeeListItemDto>));

        response.TotalCount = totalItems;

        return Results.Ok(response);
    }
}
