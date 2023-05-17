using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using ArmedMFG.ApplicationCore.Specifications.PaymentRecords;
using ArmedMFG.PublicApi.Configuration;
using ArmedMFG.PublicApi.Modules.Statistics.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.Modules.Statistics;

public class PaymentRecordsStatisticsEndpoint : IEndpoint<IResult, PaymentRecordsStatisticsRequest>
{
    private readonly IRepository<PaymentRecord> _paymentRecordsRepository;
    private readonly IRepository<PaymentCategory> _categoryRepository;
    private readonly DateParsingSettings _dateParsingSettings;
    private readonly IMapper _mapper;

    public PaymentRecordsStatisticsEndpoint(IMapper mapper,
        IRepository<PaymentRecord> paymentRecordsRepository,
        IRepository<PaymentCategory> categoryRepository,
        IOptions<DateParsingSettings> dateParsingSettings)
    {
        _mapper = mapper;
        _paymentRecordsRepository = paymentRecordsRepository;
        _categoryRepository = categoryRepository;
        _dateParsingSettings = dateParsingSettings.Value;
    }

    public async Task<IResult> HandleAsync(PaymentRecordsStatisticsRequest request)
    {
        var response = new PaymentRecordsStatisticsResponse(request.CorrelationId());

        var filterSpec = new SearchPaymentRecordFilterSpecification(
            DateTime.ParseExact(request.StartDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact(request.EndDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            0);

        var categories = await _categoryRepository.ListAsync();

        var paymentsTotal = await _paymentRecordsRepository.ListAsync(filterSpec);

        var incomeAmount = paymentsTotal.Where(p => p.PaymentCategory.Type == PaymentType.Income).Sum(p => p.Amount);
        var expenseAmount = paymentsTotal.Where(p => p.PaymentCategory.Type == PaymentType.Expense).Sum(p => p.Amount);

        foreach (var category in categories.Where(c => c.Type == PaymentType.Income))
        {
            var total = paymentsTotal.Where(p => p.PaymentCategoryId == category.Id).Sum(p => p.Amount);

            response.IncomeCategoriesInfo.Add(new(category.Id, category.Name, total));
        }

        foreach (var category in categories.Where(c => c.Type == PaymentType.Expense))
        {
            var total = paymentsTotal.Where(p => p.PaymentCategoryId == category.Id).Sum(p => p.Amount);

            response.ExpenseCategoriesInfo.Add(new(category.Id, category.Name, total));
        }


        response.IncomeAmount = incomeAmount;
        response.ExpenseAmount = expenseAmount;

        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/statistics/payment-records",
                async ([FromBody] PaymentRecordsStatisticsRequest request) =>
                {
                    return await HandleAsync(request);
                })
            .Produces<PaymentRecordsStatisticsResponse>()
            .WithTags("StatisticsEndpoint");

    }
}
