using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using ArmedMFG.ApplicationCore.Entities.ProductStockAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using ArmedMFG.PublicApi.ProductStockEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.PaymentRecordEndpoints;

public class FindListPagedPaymentRecordEndpoint : IEndpoint<IResult, FindListPagedPaymentRecordRequest>
{
    private readonly IRepository<PaymentRecord> _paymentRecordsRepository;
    private readonly IMapper _mapper;
    
    public FindListPagedPaymentRecordEndpoint(IMapper mapper, IRepository<PaymentRecord> paymentRecordsRepository)
    {
        _mapper = mapper;
        _paymentRecordsRepository = paymentRecordsRepository;
    }

    public async Task<IResult> HandleAsync(FindListPagedPaymentRecordRequest request)
    {
        var response = new FindListPagedPaymentRecordResponse(request.CorrelationId());

        var filterSpec = new PaymentRecordFilterSpecification(request.Filter.StartDate, request.Filter.EndDate, request.Filter.PaymentCategoryId);
        var totalCount = await _paymentRecordsRepository.CountAsync(filterSpec);
        
        var pagedSpec = new PaymentRecordFilterPaginatedSpecification(
            skip: (request.PageNumber.Value - 1) * request.PageSize.Value,
            take: request.PageSize.Value,
            request.Filter.StartDate,
            request.Filter.EndDate,
            request.Filter.PaymentCategoryId);

        var paymentRecords = await _paymentRecordsRepository.ListAsync(pagedSpec);
        
        response.PaymentRecords.AddRange(paymentRecords.Select(((IMapperBase)_mapper).Map<PaymentRecordDto>));
        response.TotalCount = totalCount;

        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/payment-records/find",
                async ([FromBody]FindListPagedPaymentRecordRequest request) =>
                {
                    return await HandleAsync(request);
                })
            .Produces<FindListPagedPaymentRecordResponse>()
            .WithTags("PaymentRecordEndpoint");

    }
}
