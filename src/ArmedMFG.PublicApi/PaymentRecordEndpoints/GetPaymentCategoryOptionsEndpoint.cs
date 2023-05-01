using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.MaterialTypeEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.PaymentRecordEndpoints;

public class GetPaymentCategoryOptionsEndpoint : IEndpoint<IResult, IRepository<PaymentCategory>>
{
    private readonly IMapper _mapper;

    public GetPaymentCategoryOptionsEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/payment-records/categories/input-options",
                async (IRepository<PaymentCategory> categoriesRepository) =>
                {
                    return await HandleAsync(categoriesRepository);
                })
            .Produces<GetPaymentCategoryOptionsResponse>()
            .WithTags("MaterialTypesEndpoints");
    }
    
    public async Task<IResult> HandleAsync(IRepository<PaymentCategory> categoriesRepository)
    {
        // await Task.Delay(1000);
        var response = new GetPaymentCategoryOptionsResponse();

        var categories = await categoriesRepository.ListAsync();

        foreach (var category in categories)
        {
            response.PaymentCategories.Add(new PaymentCategoryOptionDto() { Id = category.Id, Name = category.Name, Type = (byte)category.Type });
        }

        return Results.Ok(response);
    } 
}
