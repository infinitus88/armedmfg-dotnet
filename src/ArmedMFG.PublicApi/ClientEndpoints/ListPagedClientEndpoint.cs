using System;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ClientAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ClientEndpoints;

public class ListPagedClientEndpoint : IEndpoint<IResult, ListPagedClientRequest, IRepository<Client>>
{
    private readonly IMapper _mapper;

    public ListPagedClientEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/clients",
                async (int? pageSize, int? pageIndex, int? materialTypeId, IRepository<Client> clientRepository) =>
                {
                    return await HandleAsync(new ListPagedClientRequest(pageSize, pageIndex, materialTypeId), clientRepository);
                })
            .Produces<ListPagedClientResponse>()
            .WithTags("ClientEndpoints");
    }
    
    public async Task<IResult> HandleAsync(ListPagedClientRequest request, IRepository<Client> clientRepository)
    {
        await Task.Delay(1000);
        var response = new ListPagedClientResponse(request.CorrelationId());

        var filterSpec = new ClientFilterSpecification(request.OrganizationId);
        int totalItems = await clientRepository.CountAsync(filterSpec);

        var pagedSpec = new ClientFilterPaginatedSpecification(
            skip: request.PageIndex.Value * request.PageSize.Value,
            take: request.PageSize.Value,
            organizationId: request.OrganizationId);

        var clients = await clientRepository.ListAsync(pagedSpec);

        response.Clients.AddRange(clients.Select(((IMapperBase)_mapper).Map<ClientDto>));

        if (request.PageSize > 0)
        {
            response.PageCount = int.Parse(Math.Ceiling((decimal)totalItems / request.PageSize.Value).ToString());
        }
        else
        {
            response.PageCount = totalItems > 0 ? 1 : 0;
        }

        return Results.Ok(response);
    }
}
