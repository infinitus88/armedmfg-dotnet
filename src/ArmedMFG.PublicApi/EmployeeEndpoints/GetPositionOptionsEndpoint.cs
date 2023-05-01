using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.EmployeeAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.EmployeeEndpoints;

public class GetPositionOptionsEndpoint : IEndpoint<IResult, IRepository<EmployeePosition>>
{
    private readonly IMapper _mapper;

    public GetPositionOptionsEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/employees/positions/input-options",
                async (IRepository<EmployeePosition> positionRepository) =>
                {
                    return await HandleAsync(positionRepository);
                })
            .Produces<GetPositionOptionsResponse>()
            .WithTags("EmployeesEndpoints");
    }
    
    public async Task<IResult> HandleAsync(IRepository<EmployeePosition> positionRepository)
    {
        // await Task.Delay(1000);
        var response = new GetPositionOptionsResponse();

        var products = await positionRepository.ListAsync();

        foreach (var product in products)
        {
            response.Positions.Add(new PositionOptionDto() { Id = product.Id, Name = product.Name });
        }

        return Results.Ok(response);
    } 
}
