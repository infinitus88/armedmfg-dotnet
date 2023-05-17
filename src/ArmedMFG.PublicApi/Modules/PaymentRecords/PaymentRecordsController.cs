using System.Threading.Tasks;
using ArmedMFG.PublicApi.Modules.PaymentRecords.Dtos.Shared;
using ArmedMFG.PublicApi.Modules.PaymentRecords.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;
using AutoMapper;
using ArmedMFG.PublicApi.Configuration;
using System.Globalization;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ArmedMFG.ApplicationCore.Specifications.PaymentRecords;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ArmedMFG.PublicApi.Modules.PaymentRecords;
[Route("api/payment-records")]
[ApiController]
public class PaymentRecordsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly DateParsingSettings _dateParsingSettings;
    private readonly IRepository<PaymentRecord> _paymentRecordRepository;
    private readonly IRepository<PaymentCategory> _categoryRepository;

    public PaymentRecordsController(IMapper mapper, DateParsingSettings dateParsingSettings, IRepository<PaymentRecord> paymentRecordRepository, IRepository<PaymentCategory> categoryRepository)
    {
        _mapper = mapper;
        _dateParsingSettings = dateParsingSettings;
        _paymentRecordRepository = paymentRecordRepository;
        _categoryRepository = categoryRepository;
    }

    [HttpPost]
    [Obsolete]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> Create([FromBody] CreatePaymentRecordRequest request)
    {
        var response = new CreatePaymentRecordResponse(request.CorrelationId());
        var newItem = new PaymentRecord(
            DateTime.ParseExact(request.PayedDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            request.PaymentCategoryId, request.ReferenceId, 
            (PaymentMethod)request.PaymentMethod,
            request.Description,
            request.Amount);

        newItem = await _paymentRecordRepository.AddAsync(newItem);

        var paymentRecordWithCategory = await _paymentRecordRepository.GetBySpecAsync(new PaymentRecordWithCategorySpecification(newItem.Id));


        response.PaymentRecord = _mapper.Map<PaymentRecordDto>(paymentRecordWithCategory);
        return Results.Created($"api/payment-records/{newItem.Id}", response);
    }

    [HttpGet("categories/options")]
    public async Task<IResult> GetCategoryOptions()
    {
        var response = new GetPaymentCategoryOptionsResponse();

        var categories = await _categoryRepository.ListAsync();

        response.PaymentCategories.AddRange(categories.Select(_mapper.Map<PaymentCategoryOptionDto>));
        
        return Results.Ok(response);
    }
    
    [HttpGet("{paymentRecordId}")]
    public async Task<IResult> GetPaymentRecordForEdit(int paymentRecordId)
    {
        var response = new GetPaymentRecordForEditResponse();

        var paymentRecordForEdit = await _paymentRecordRepository.GetByIdAsync(paymentRecordId);
        if (paymentRecordForEdit is null)
            return Results.NotFound();

        response.PaymentRecord = _mapper.Map<PaymentRecordForEditDto>(paymentRecordForEdit);

        return Results.Ok(response);
    }

    [HttpPost("search")]
    public async Task<IResult> Search([FromBody] SearchPaymentRecordsRequest request)
    {
        var response = new SearchPaymentRecordsResponse(request.CorrelationId());

        var filterSpec = new SearchPaymentRecordFilterSpecification(
            DateTime.ParseExact(request.Filter.StartDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact(request.Filter.EndDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            request.Filter.PaymentCategoryId);

        var paymentsTotal = await _paymentRecordRepository.ListAsync(filterSpec);
        var totalCount = await _paymentRecordRepository.CountAsync(filterSpec);
        var incomeAmount = paymentsTotal.Where(p => p.PaymentCategory.Type == PaymentType.Income).Sum(p => p.Amount);
        var expenseAmount = paymentsTotal.Where(p => p.PaymentCategory.Type == PaymentType.Expense).Sum(p => p.Amount);

        var pagedSpec = new SearchPaymentRecordFilterPaginatedSpecification(
            skip: (request.PageNumber.Value - 1) * request.PageSize.Value,
            take: request.PageSize.Value,
            DateTime.ParseExact(request.Filter.StartDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact(request.Filter.EndDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            request.Filter.PaymentCategoryId);

        var paymentRecords = await _paymentRecordRepository.ListAsync(pagedSpec);

        response.PaymentRecords.AddRange(paymentRecords.Select(((IMapperBase)_mapper).Map<PaymentRecordDto>));
        response.TotalCount = totalCount;
        response.IncomeAmount = incomeAmount;
        response.ExpenseAmount = expenseAmount;

        return Results.Ok(response);
    }

    [HttpPut]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> Update([FromBody] UpdatePaymentRecordRequest request)
    {
        var response = new UpdatePaymentRecordResponse(request.CorrelationId());

        var existingPaymentRecord = await _paymentRecordRepository.GetByIdAsync(request.Id);

        if (existingPaymentRecord is null)
            return Results.NotFound();

        PaymentRecord.PaymentRecordDetails details = new(
            DateTime.ParseExact(request.PayedDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            request.ReferenceId,
            (PaymentMethod)request.PaymentMethod,
            request.Amount,
            request.Description);
        existingPaymentRecord.UpdateDetails(details);

        await _paymentRecordRepository.UpdateAsync(existingPaymentRecord);

        response.PaymentRecord = _mapper.Map<PaymentRecordDto>(existingPaymentRecord);

        return Results.Ok(response);
    }

    [HttpDelete("{paymentRecordId}")]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> SingleDelete(int paymentRecordId)
    {
        var response = new DeleteSinglePaymentRecordResponse();

        var recordToDelete = await _paymentRecordRepository.GetByIdAsync(paymentRecordId);
        if (recordToDelete is null)
            return Results.NotFound();

        await _paymentRecordRepository.DeleteAsync(recordToDelete);

        return Results.Ok(response);
    }
}
