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
using ArmedMFG.PublicApi.Modules.ProducedProducts.Dtos;
using ArmedMFG.PublicApi.Modules.ProducedProducts.Dtos.SharedDtos;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.PublicApi.Modules.OrderShipments.Dtos;
using System.Globalization;
using ArmedMFG.PublicApi.Modules.OrderShipments.Dtos.SharedDtos;
using ArmedMFG.ApplicationCore.Specifications.OrderShipments;

namespace ArmedMFG.PublicApi.Modules.OrderShipments;
[Route("api/order-shipments")]
[ApiController]
public class OrderShipmentsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly DateParsingSettings _dateParsingSettings;
    private readonly IRepository<OrderShipment> _shipmentRepository;

    public OrderShipmentsController(IMapper mapper, DateParsingSettings dateParsingSettings, IRepository<OrderShipment> shipmentRepository)
    {
        _mapper = mapper;
        _dateParsingSettings = dateParsingSettings;
        _shipmentRepository = shipmentRepository;
    }

    [HttpPost]
    [Obsolete]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> Create([FromBody] CreateOrderShipmentRequest request)
    {
        var response = new CreateOrderShipmentResponse(request.CorrelationId());

        var newShipment = new OrderShipment(
            request.OrderId,
            DateTime.ParseExact(request.ShipmentDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            request.DriverName,
            request.DriverPhone,
            request.CarNumber,
            request.Destination
            );

        newShipment = await _shipmentRepository.AddAsync(newShipment);

        response.OrderShipment = _mapper.Map<OrderShipmentDto>(newShipment);
        return Results.Created($"api/order-shipments/{newShipment.Id}", response);
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetOrderShipmentForEdit(int id)
    {
        var response = new GetProducedProductForEditResponse();

        var producedProductForEdit = await _shipmentRepository.GetByIdAsync(id);
        if (producedProductForEdit is null)
            return Results.NotFound();

        response.ProducedProduct = _mapper.Map<ProducedProductForEditDto>(producedProductForEdit);

        return Results.Ok(response);
    }

    [HttpPost("search")]
    public async Task<IResult> Search([FromBody] SearchOrderShipmentRequest request)
    {
        var response = new SearchOrderShipmentResponse(request.CorrelationId());

        var filterSpec = new SearchOrderShipmentFilterSpecification(
            DateTime.ParseExact(request.Filter.StartDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact(request.Filter.EndDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            request.Filter.OrderId,
            request.Filter.SearchText);

        var totalItems = await _shipmentRepository.CountAsync(filterSpec);

        var pagedSpec = new SearchOrderShipmentFilterPaginatedSpecification(
            skip: (request.PageNumber - 1) * request.PageSize,
            take: request.PageSize,
            DateTime.ParseExact(request.Filter.StartDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact(request.Filter.EndDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            request.Filter.OrderId,
            request.Filter.SearchText);

        var customers = await _shipmentRepository.ListAsync(pagedSpec);

        response.OrderShipments.AddRange(customers.Select(_mapper.Map<OrderShipmentDto>));
        response.TotalCount = totalItems;

        return Results.Ok(response);
    }

    [HttpPut]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> Update([FromBody] UpdateOrderShipmentRequest request)
    {
        var response = new UpdateOrderShipmentResponse(request.CorrelationId());

        var existingShipment = await _shipmentRepository.GetByIdAsync(request.Id);

        if (existingShipment is null)
            return Results.NotFound();

        OrderShipment.OrderShipmentDetails details = new(
            DateTime.ParseExact(request.ShipmentDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            request.DriverName,
            request.DriverPhone,
            request.CarNumber,
            request.Destination);
        existingShipment.UpdateDetails(details);

        await _shipmentRepository.UpdateAsync(existingShipment);

        response.OrderShipment = _mapper.Map<OrderShipmentDto>(existingShipment);

        return Results.Ok(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> SingleDelete(int id)
    {
        var response = new DeleteSingleOrderShipmentResponse();

        var shipmentToDelete = await _shipmentRepository.GetByIdAsync(id);
        if (shipmentToDelete is null)
            return Results.NotFound();

        await _shipmentRepository.DeleteAsync(shipmentToDelete);

        return Results.Ok(response);
    }
}
