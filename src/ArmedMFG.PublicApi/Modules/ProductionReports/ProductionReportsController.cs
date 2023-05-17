using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ArmedMFG.ApplicationCore.Interfaces;
using AutoMapper;
using ArmedMFG.PublicApi.Configuration;
using System.Globalization;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ArmedMFG.ApplicationCore.Entities.ProductionReport;
using ArmedMFG.PublicApi.Modules.ProductionReports.Dtos;
using ArmedMFG.PublicApi.Modules.ProductionReports.Dtos.SharedDtos;
using ArmedMFG.ApplicationCore.Specifications.ProductionReports;
using ArmedMFG.ApplicationCore.Entities.ProductAggregate;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;

namespace ArmedMFG.PublicApi.Modules.ProductionReports;
[Route("api/production-reports")]
[ApiController]
public class ProductionReportsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly DateParsingSettings _dateParsingSettings;
    private readonly IRepository<ProductionReport> _reportRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<MaterialAmountHistory> _materialHistoryRepository;

    public ProductionReportsController(IMapper mapper, DateParsingSettings dateParsingSettings, IRepository<ProductionReport> reportRepository, IRepository<Product> productRepository, IRepository<MaterialAmountHistory> materialHistoryRepository)
    {
        _mapper = mapper;
        _dateParsingSettings = dateParsingSettings;
        _reportRepository = reportRepository;
        _productRepository = productRepository;
        _materialHistoryRepository = materialHistoryRepository;
    }

    [HttpPost("create")]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> Create([FromBody] CreateProductionReportRequest request)
    {
        var response = new CreateProductionReportResponse(request.CorrelationId());

        var newReport = new ProductionReport(
            DateTime.ParseExact(request.ReportDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture));

        newReport.AddProductRange(request.ProducedProducts.Select(p => new ProducedProduct(p.ProductId, p.Quantity)).ToList());

        newReport = await _reportRepository.AddAsync(newReport);

        var filterSpec = new ProductionReportWithProducedProductSpecification(newReport.Id);
        var createdReport = await _reportRepository.GetBySpecAsync(filterSpec);

        var materialHistory = await _materialHistoryRepository.ListAsync();

        // Record Material usage
        foreach (var product in createdReport.ProducedProducts)
        {
            foreach (var material in product.Product.ProductMaterials)
            {
                var materialLastAmount = materialHistory.Where(m => m.MaterialId == material.MaterialId).Last().ToAmount;

                await _materialHistoryRepository.AddAsync(new MaterialAmountHistory(
                    newReport.ReportDate, material.MaterialId,
                    materialLastAmount,
                    materialLastAmount + (product.Quantity * material.Amount),
                    MaterialAmountChangeType.UsedForProduct
                    ));
            }
        }

        response.ProductionReport = _mapper.Map<ProductionReportDto>(newReport);

        return Results.Created($"api/production-reports/{newReport.Id}", response);
    }

    [HttpGet("{reportId}")]
    public async Task<IResult> GetReportForEdit(int reportId)
    {
        var response = new GetProductionReportForEditResponse();

        var reportForEdit = await _reportRepository.GetByIdAsync(reportId);
        if (reportForEdit is null)
            return Results.NotFound();

        response.ProductionReport = _mapper.Map<ProductionReportForEditDto>(reportForEdit);

        return Results.Ok(response);
    }

    [HttpPost("search")]
    public async Task<IResult> Search([FromBody] SearchProductionReportRequest request)
    {
        var response = new SearchProductionReportResponse(request.CorrelationId());

        var filterSpec = new SearchProductionReportFilterSpecification(
            request.Filter.SearchText, 
            DateTime.ParseExact(request.Filter.StartDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact(request.Filter.EndDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture));
        var totalItems = await _reportRepository.CountAsync(filterSpec);

        var pagedSpec = new SearchProductionReportFilterPaginatedSpecification(
            skip: (request.PageNumber - 1) * request.PageSize,
            take: request.PageSize,
            request.Filter.SearchText,
            DateTime.ParseExact(request.Filter.StartDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact(request.Filter.EndDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture));

        var reports = await _reportRepository.ListAsync(pagedSpec);

        response.ProductionReports.AddRange(reports.Select(_mapper.Map<ProductionReportDto>));
        response.TotalCount = totalItems;

        return Results.Ok(response);
    }

    [HttpPut]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> Update([FromBody] UpdateProductionReportRequest request)
    {
        var response = new UpdateProductionReportResponse(request.CorrelationId());

        var existingReport = await _reportRepository.GetByIdAsync(request.Id);

        if (existingReport is null)
            return Results.NotFound();

        ProductionReport.ProductionReportDetails details = new(
            DateTime.ParseExact(request.ReportDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture));

        existingReport.UpdateDetails(details);

        await _reportRepository.UpdateAsync(existingReport);

        response.ProductionReport = _mapper.Map<ProductionReportDto>(existingReport);

        return Results.Ok(response);
    }

    [HttpDelete("{reportId}")]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> SingleDelete(int reportId)
    {
        var response = new DeleteSingleProductionReportResponse();

        var reportToDelete = await _reportRepository.GetByIdAsync(reportId);
        if (reportToDelete is null)
            return Results.NotFound();

        await _reportRepository.DeleteAsync(reportToDelete);

        return Results.Ok(response);
    }
}
