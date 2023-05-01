using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using ArmedMFG.PublicApi.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.PaymentRecordEndpoints;

public class SearchPaymentRecordsEndpoint : IEndpoint<IResult, SearchPaymentRecordsRequest>
{
    private readonly IRepository<PaymentRecord> _paymentRecordsRepository;
    private readonly DateParsingSettings _dateParsingSettings;
    private readonly IMapper _mapper;
    
    public SearchPaymentRecordsEndpoint(IMapper mapper, IRepository<PaymentRecord> paymentRecordsRepository, IOptions<DateParsingSettings> dateParsingSettings)
    {
        _mapper = mapper;
        _paymentRecordsRepository = paymentRecordsRepository;
        _dateParsingSettings = dateParsingSettings.Value;
    }

    public async Task<IResult> HandleAsync(SearchPaymentRecordsRequest request)
    {
        var response = new SearchPaymentRecordsResponse(request.CorrelationId());

        var filterSpec = new PaymentRecordFilterSpecification(
            DateTime.ParseExact(request.Filter.StartDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact(request.Filter.EndDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            request.Filter.PaymentCategoryId);
        var paymentsTotal = await _paymentRecordsRepository.ListAsync(filterSpec); 
        var totalCount = await _paymentRecordsRepository.CountAsync(filterSpec);
        var incomeAmount = paymentsTotal.Where(p => p.PaymentCategory.Type == PaymentType.Income).Sum(p => p.Amount);
        var expenseAmount = paymentsTotal.Where(p => p.PaymentCategory.Type == PaymentType.Expense).Sum(p => p.Amount);
        
        var pagedSpec = new PaymentRecordFilterPaginatedSpecification(
            skip: (request.PageNumber.Value - 1) * request.PageSize.Value,
            take: request.PageSize.Value,
            DateTime.ParseExact(request.Filter.StartDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact(request.Filter.EndDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            request.Filter.PaymentCategoryId);

        var paymentRecords = await _paymentRecordsRepository.ListAsync(pagedSpec);
        
        response.PaymentRecords.AddRange(paymentRecords.Select(((IMapperBase)_mapper).Map<PaymentRecordDto>));
        response.TotalCount = totalCount;
        response.IncomeAmount = incomeAmount;
        response.ExpenseAmount = expenseAmount;

        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/payment-records/search",
                async ([FromBody]SearchPaymentRecordsRequest request) =>
                {
                    return await HandleAsync(request);
                })
            .Produces<SearchPaymentRecordsResponse>()
            .WithTags("PaymentRecordEndpoint");

    }
}
