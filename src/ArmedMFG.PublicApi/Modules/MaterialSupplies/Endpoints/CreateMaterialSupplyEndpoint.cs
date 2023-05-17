using System;
using System.Globalization;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;
using ArmedMFG.ApplicationCore.Exceptions;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.Configuration;
using ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos;
using ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos.SharedDtos;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.Modules.MaterialSupplies.Endpoints;

public class CreateMaterialSupplyEndpoint : IEndpoint<IResult, CreateMaterialSupplyRequest>
{
    private readonly IMapper _mapper;
    private readonly DateParsingSettings _dateParsingSettings;
    private readonly IRepository<MaterialSupply> _materialSupplyRepository;
    public CreateMaterialSupplyEndpoint(IMapper mapper, IOptions<DateParsingSettings> dateParsingOptions, IRepository<MaterialSupply> materialSupplyRepository)
    {
        _mapper = mapper;
        _dateParsingSettings = dateParsingOptions.Value;
        _materialSupplyRepository = materialSupplyRepository;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/materials/supplies",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (CreateMaterialSupplyRequest request) =>
                {
                    return await HandleAsync(request);
                })
            .Produces<CreateMaterialSupplyResponse>()
            .WithTags("MaterialSupplyEndpoints");
    }

    public async Task<IResult> HandleAsync(CreateMaterialSupplyRequest request)
    {
        var response = new CreateMaterialSupplyResponse(request.CorrelationId());

        var newItem = new MaterialSupply(
            request.MaterialId,
            DateTime.ParseExact(request.DeliveredDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            request.TotalPrice,
            request.Amount);

        newItem = await _materialSupplyRepository.AddAsync(newItem);

        response.MaterialSupply = _mapper.Map<MaterialSupplyDto>(newItem);

        return Results.Created($"api/materials/supplies/{newItem.Id}", response);
    }
}
