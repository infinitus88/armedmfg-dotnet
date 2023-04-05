using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.PaymentRecordEndpoints;

public class CreatePaymentRecordEndpoint : IEndpoint<IResult, CreatePaymentRecordRequest, IRepository<PaymentRecord>>
{
    public CreatePaymentRecordEndpoint()
    {
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/payment-records",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (CreatePaymentRecordRequest request, IRepository<PaymentRecord> paymentRecordRepository) =>
                {
                    return await HandleAsync(request, paymentRecordRepository);
                })
            .Produces<CreatePaymentRecordResponse>()
            .WithTags("PaymentRecordEndpoints");
    }

    public async Task<IResult> HandleAsync(CreatePaymentRecordRequest request, IRepository<PaymentRecord> paymentRecordRepository)
    {
        var response = new CreatePaymentRecordResponse(request.CorrelationId());

        var newItem = new PaymentRecord(request.Data.PayedDate, request.Data.PaymentCategoryId, request.Data.ReferenceId, request.Data.PaymentMethod,
            request.Data.Description, request.Data.Amount);
        newItem = await paymentRecordRepository.AddAsync(newItem);

        var dto = new PaymentRecordDto
        {
            Id = newItem.Id,
            PayedDate = newItem.PayedDate,
            PaymentCategoryId = newItem.PaymentCategoryId,
            ReferenceId = newItem.ReferenceId,
            Description = newItem.Description,
            Amount = newItem.Amount
        };
        response.PaymentRecord = dto;
        return Results.Created($"api/payment-records/{dto.Id}", response);
    }
}
