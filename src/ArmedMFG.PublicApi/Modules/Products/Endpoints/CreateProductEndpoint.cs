using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductAggregate;
using ArmedMFG.ApplicationCore.Exceptions;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications.Products;
using ArmedMFG.PublicApi.Modules.Products.Dtos;
using ArmedMFG.PublicApi.Modules.Products.Dtos.SharedDtos;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class CreateProductEndpoint : IEndpoint<IResult, CreateProductRequest>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Product> _productRepository;

    public CreateProductEndpoint(IMapper mapper, IRepository<Product> productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public  async Task<IResult> HandleAsync(CreateProductRequest request)
    {
        var response = new CreateProductResponse(request.CorrelationId());

        var productNameSpecification = new ProductNameSpecification(request.Name);
        var existingProduct = await _productRepository.CountAsync(productNameSpecification);
        if (existingProduct > 0)
        {
            throw new DuplicateException($"A product with name {request.Name} already exists");
        }

        var newItem = new Product(request.ProductCategoryId, request.Name, request.Quantity, request.UnitPrice);
        newItem.AddRangeProductMaterials(request.ProductMaterials.Select(p => new ProductMaterial(p.MaterialId, p.Amount)));
        newItem = await _productRepository.AddAsync(newItem);

        response.Product = _mapper.Map<ProductDto>(newItem);
        return Results.Created($"api/products/{newItem.Id}", response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/products",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (CreateProductRequest request) =>
                {
                    return await HandleAsync(request);
                })
            .Produces<CreateProductResponse>()
            .WithTags("ProductEndpoints");
    }
  }
