using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ArmedMFG.ApplicationCore.Interfaces;
using AutoMapper;
using ArmedMFG.PublicApi.Configuration;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ArmedMFG.PublicApi.Modules.ProducedProducts.Dtos;
using ArmedMFG.PublicApi.Modules.ProducedProducts.Dtos.SharedDtos;
using ArmedMFG.ApplicationCore.Entities.ProductionReport;
using ArmedMFG.ApplicationCore.Specifications.ProducedProducts;
using ArmedMFG.ApplicationCore.Exceptions;

namespace ArmedMFG.PublicApi.Modules.ProducedProducts;
[Route("api/produced-products")]
[ApiController]
public class ProducedProductsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly DateParsingSettings _dateParsingSettings;
    private readonly IRepository<ProducedProduct> _producedProductRepository;

    public ProducedProductsController(IMapper mapper, DateParsingSettings dateParsingSettings, IRepository<ProducedProduct> producedProductRepository)
    {
        _mapper = mapper;
        _dateParsingSettings = dateParsingSettings;
        _producedProductRepository = producedProductRepository;
    }

    [HttpPost]
    [Obsolete]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> Create([FromBody] CreateProducedProductRequest request)
    {
        var response = new CreateProducedProductResponse(request.CorrelationId());

        var filterSpec = new ProducedProductUniqueSpecification(request.ProductionReportId, request.ProductId);
        var existingProduct = await _producedProductRepository.CountAsync(filterSpec);

        if (existingProduct > 0) 
        {
            throw new DuplicateException($"A produced product with productId {request.ProductId} already exist");
        }

        var newProducedProduct = new ProducedProduct(
            request.ProductionReportId,
            request.ProductId,
            request.Quantity
            );

        newProducedProduct = await _producedProductRepository.AddAsync(newProducedProduct);

        response.ProducedProduct = _mapper.Map<ProducedProductDto>(newProducedProduct);
        return Results.Created($"api/produced-products/{newProducedProduct.Id}", response);
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetProducedProductForEdit(int id)
    {
        var response = new GetProducedProductForEditResponse();

        var producedProductForEdit = await _producedProductRepository.GetByIdAsync(id);
        if (producedProductForEdit is null)
            return Results.NotFound();

        response.ProducedProduct = _mapper.Map<ProducedProductForEditDto>(producedProductForEdit);

        return Results.Ok(response);
    }

    [HttpPost("search")]
    public async Task<IResult> Search([FromBody] SearchProducedProductRequest request)
    {
        var response = new SearchProducedProductResponse(request.CorrelationId());

        var filterSpec = new SearchProducedProductFilterSpecification(request.Filter.ProductionReportId, request.Filter.SearchText);
        var totalItems = await _producedProductRepository.CountAsync(filterSpec);

        var pagedSpec = new SearchProducedProductFilterPaginatedSpecification(
            skip: (request.PageNumber - 1) * request.PageSize,
            take: request.PageSize,
            request.Filter.ProductionReportId,
            request.Filter.SearchText);

        var customers = await _producedProductRepository.ListAsync(pagedSpec);

        response.ProducedProducts.AddRange(customers.Select(_mapper.Map<ProducedProductDto>));
        response.TotalCount = totalItems;

        return Results.Ok(response);
    }

    [HttpPut]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> Update([FromBody] UpdateProducedProductRequest request)
    {
        var response = new UpdateProducedProductResponse(request.CorrelationId());

        var existingProducedProduct = await _producedProductRepository.GetByIdAsync(request.Id);

        if (existingProducedProduct is null)
            return Results.NotFound();

        ProducedProduct.ProducedProductDetails details = new(
            request.ProductId,
            request.Quantity);
        existingProducedProduct.UpdateDetails(details);

        await _producedProductRepository.UpdateAsync(existingProducedProduct);

        response.ProducedProduct = _mapper.Map<ProducedProductDto>(existingProducedProduct);

        return Results.Ok(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> SingleDelete(int id)
    {
        var response = new DeleteSingleProducedProductResponse();

        var producedProductToDelete = await _producedProductRepository.GetByIdAsync(id);
        if (producedProductToDelete is null)
            return Results.NotFound();

        await _producedProductRepository.DeleteAsync(producedProductToDelete);

        return Results.Ok(response);
    }
}
