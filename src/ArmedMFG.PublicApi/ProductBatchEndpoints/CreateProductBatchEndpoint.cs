﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
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

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class CreateProductBatchEndpoint : IEndpoint<IResult, CreateProductBatchRequest, IRepository<ProductBatch>>
{
    private readonly IMapper _mapper;
    private readonly DateParsingSettings _dateParsingSettings;

    public CreateProductBatchEndpoint(IMapper mapper, IOptions<DateParsingSettings> dateParsingSettings)
    {
        _mapper = mapper;
        _dateParsingSettings = dateParsingSettings.Value;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/product-batches",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS,
                    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
                async
                    (CreateProductBatchRequest request, IRepository<ProductBatch> productBatchRepository) =>
                {
                    return await HandleAsync(request, productBatchRepository);
                })
            .Produces<CreateProductBatchResponse>()
            .WithTags("ProductBatchEndpoints");
    }
    
    public async Task<IResult> HandleAsync(CreateProductBatchRequest request,
        IRepository<ProductBatch> productBatchRepository)
    {
        var response = new CreateProductBatchResponse(request.CorrelationId());

        var newBatch = new ProductBatch(DateTime.ParseExact(request.ProducedDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture));

        newBatch.AddProductRange(request.ProducedProducts.Select(p => new ProducedProduct(p.ProductTypeId, p.Quantity)).ToList());
        newBatch.AddMaterialRange(request.SpentMaterials.Select(m => new SpentMaterial(m.MaterialTypeId, m.Amount)).ToList());
        newBatch = await productBatchRepository.AddAsync(newBatch);

        var dto = new ProductBatchDto
        {
            Id = newBatch.Id,
            ProducedDate = newBatch.ProducedDate.ToString(_dateParsingSettings.DefaultDisplayDateFormat),
            ProducedProducts = newBatch.ProducedProducts.Select(_mapper.Map<ProducedProductDto>).ToList(),
            SpentMaterials = newBatch.SpentMaterials.Select(_mapper.Map<SpentMaterialDto>).ToList()
        };
        response.ProductBatch = dto;
        return Results.Created($"api/product-batches/{dto.Id}", response);
    }
}
