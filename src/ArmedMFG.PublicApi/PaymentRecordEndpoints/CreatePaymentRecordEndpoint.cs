using System;
using System.Globalization;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using ArmedMFG.PublicApi.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.PaymentRecordEndpoints;

public class CreatePaymentRecordEndpoint : IEndpoint<IResult, CreatePaymentRecordRequest, IRepository<PaymentRecord>>
{
    private readonly DateParsingSettings _dateParsingSettings;
    private readonly IMapper _mapper;
    public CreatePaymentRecordEndpoint(IMapper mapper, IOptions<DateParsingSettings> dateParsingSettings)
    {
        _mapper = mapper;
        _dateParsingSettings = dateParsingSettings.Value;
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

        var newItem = new PaymentRecord(
            DateTime.ParseExact(request.Data.PayedDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture), 
            request.Data.PaymentCategoryId, request.Data.ReferenceId, (PaymentMethod)request.Data.PaymentMethod,
            request.Data.Description, request.Data.Amount);
        newItem = await paymentRecordRepository.AddAsync(newItem);

        var paymentRecordWithCategory = await paymentRecordRepository.GetBySpecAsync(new PaymentRecordWithCategorySpecification(newItem.Id));


        response.PaymentRecord = _mapper.Map<PaymentRecordDto>(paymentRecordWithCategory);
        return Results.Created($"api/payment-records/{newItem.Id}", response);
    }
}
