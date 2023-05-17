using System;
using System.Globalization;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;
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

public class UpdateMaterialSupplyEndpoint : IEndpoint<IResult, UpdateMaterialSupplyRequest>
{
    private readonly IMapper _mapper;
    private readonly DateParsingSettings _dateParsingSettings;
    private readonly IRepository<MaterialSupply> _materialSupplyRepository;
    public UpdateMaterialSupplyEndpoint(IMapper mapper, IOptions<DateParsingSettings> dateParsingOptions, IRepository<MaterialSupply> materialSupplyRepository)
    {
        _mapper = mapper;
        _dateParsingSettings = dateParsingOptions.Value;
        _materialSupplyRepository = materialSupplyRepository;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/materials/supplies",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (UpdateMaterialSupplyRequest request) =>
                {
                    return await HandleAsync(request);
                })
            .Produces<UpdateMaterialSupplyResponse>()
            .WithTags("MaterialSupplyEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateMaterialSupplyRequest request)
    {
        var response = new UpdateMaterialSupplyResponse(request.CorrelationId());

        var existingMaterialSupply = await _materialSupplyRepository.GetByIdAsync(request.Id);

        if (existingMaterialSupply is null)
            return Results.NotFound();

        MaterialSupply.MaterialSupplyDetails details = new(
            DateTime.ParseExact(request.DeliveredDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            request.TotalPrice,
            request.Amount);

        existingMaterialSupply.UpdateDetails(details);

        await _materialSupplyRepository.UpdateAsync(existingMaterialSupply);

        response.MaterialSupply = _mapper.Map<MaterialSupplyDto>(existingMaterialSupply);

        return Results.Ok(response);
    }
}
