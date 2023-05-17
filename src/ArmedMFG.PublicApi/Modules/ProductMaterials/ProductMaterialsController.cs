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
using ArmedMFG.ApplicationCore.Entities.ProductAggregate;
using ArmedMFG.PublicApi.Modules.ProductMaterials.Dtos;
using ArmedMFG.PublicApi.Modules.ProductMaterials.Dtos.SharedDtos;
using ArmedMFG.ApplicationCore.Specifications.ProductMaterials;
using ArmedMFG.ApplicationCore.Exceptions;

namespace ArmedMFG.PublicApi.Modules.ProductMaterials;
[Route("api/product-materials")]
[ApiController]
public class ProductMaterialsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly DateParsingSettings _dateParsingSettings;
    private readonly IRepository<ProductMaterial> _productMaterialRepository;

    public ProductMaterialsController(IMapper mapper, DateParsingSettings dateParsingSettings, IRepository<ProductMaterial> productMaterialRepository)
    {
        _mapper = mapper;
        _dateParsingSettings = dateParsingSettings;
        _productMaterialRepository = productMaterialRepository;
    }

    [HttpPost]
    [Obsolete]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> Create([FromBody] CreateProductMaterialRequest request)
    {
        var response = new CreateProductMaterialResponse(request.CorrelationId());

        var filterSpec = new ProductMaterialUniqueSpecification(request.ProductId, request.MaterialId);

        var existingMaterial = await _productMaterialRepository.CountAsync(filterSpec);

        if (existingMaterial > 0)
        {
            throw new DuplicateException($"A product material with materialId {request.MaterialId} already exist");
        }

        var newProductMaterial = new ProductMaterial(
            request.ProductId,
            request.MaterialId,
            request.Amount
            );

        newProductMaterial = await _productMaterialRepository.AddAsync(newProductMaterial);

        response.ProductMaterial = _mapper.Map<ProductMaterialDto>(newProductMaterial);
        return Results.Created($"api/product-materials/{newProductMaterial.Id}", response);
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetPaymentRecordForEdit(int id)
    {
        var response = new GetProductMaterialForEditResponse();

        var productMaterialForEdit = await _productMaterialRepository.GetByIdAsync(id);
        if (productMaterialForEdit is null)
            return Results.NotFound();

        response.ProductMaterial = _mapper.Map<ProductMaterialForEditDto>(productMaterialForEdit);

        return Results.Ok(response);
    }

    [HttpPost("search")]
    public async Task<IResult> Search([FromBody] SearchProductMaterialRequest request)
    {
        var response = new SearchProductMaterialResponse(request.CorrelationId());

        var filterSpec = new SearchProductMaterialFilterSpecification(request.Filter.ProductId, request.Filter.SearchText);
        var totalItems = await _productMaterialRepository.CountAsync(filterSpec);

        var pagedSpec = new SearchProductMaterialFilterPaginatedSpecification(
            skip: (request.PageNumber - 1) * request.PageSize,
            take: request.PageSize,
            request.Filter.ProductId,
            request.Filter.SearchText);

        var customers = await _productMaterialRepository.ListAsync(pagedSpec);

        response.ProductMaterials.AddRange(customers.Select(_mapper.Map<ProductMaterialDto>));
        response.TotalCount = totalItems;

        return Results.Ok(response);
    }

    [HttpPut]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> Update([FromBody] UpdateProductMaterialRequest request)
    {
        var response = new UpdateProductMaterialResponse(request.CorrelationId());

        var existingProductMaterial = await _productMaterialRepository.GetByIdAsync(request.Id);

        if (existingProductMaterial is null)
            return Results.NotFound();

        ProductMaterial.ProductMaterialDetails details = new(
            request.MaterialId,
            request.Amount);
        existingProductMaterial.UpdateDetails(details);

        await _productMaterialRepository.UpdateAsync(existingProductMaterial);

        response.ProductMaterial = _mapper.Map<ProductMaterialDto>(existingProductMaterial);

        return Results.Ok(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> SingleDelete(int id)
    {
        var response = new DeleteSingleProductMaterialResponse();

        var productMaterialToDelete = await _productMaterialRepository.GetByIdAsync(id);
        if (productMaterialToDelete is null)
            return Results.NotFound();

        await _productMaterialRepository.DeleteAsync(productMaterialToDelete);

        return Results.Ok(response);
    }
}
