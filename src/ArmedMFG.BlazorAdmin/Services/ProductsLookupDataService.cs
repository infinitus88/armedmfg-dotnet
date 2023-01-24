using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared;
using ArmedMFG.BlazorShared.Attributes;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ArmedMFG.BlazorAdmin.Services;

public class ProductsLookupDataService<TLookupData, TResponse>
    : IProductsLookupDataService<TLookupData>
    where TLookupData : LookupData
    where TResponse : ILookupDataResponse<TLookupData>
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ProductsLookupDataService<TLookupData, TResponse>> _logger;
    private readonly string _apiUrl;

    public ProductsLookupDataService(HttpClient httpClient,
        IOptions<BaseUrlConfiguration> baseUrlConfiguration,
        ILogger<ProductsLookupDataService<TLookupData, TResponse>> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _apiUrl = baseUrlConfiguration.Value.ApiBase;
    }

    public async Task<List<TLookupData>> List()
    {
        var endpointName = typeof(TLookupData).GetCustomAttribute<EndpointAttribute>().Name;
        _logger.LogInformation($"Fetching {typeof(TLookupData).Name} from API. Endpoint : {endpointName}");

        var response = await _httpClient.GetFromJsonAsync<TResponse>($"{_apiUrl}{endpointName}");
        return response.List;
    }
}
