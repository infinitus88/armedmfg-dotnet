using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class MaterialTypeService : IMaterialTypeService
{
    private readonly IProductsLookupDataService<MaterialCategory> _categoryService;
    private readonly HttpService _httpService;
    private readonly ILogger<MaterialTypeService> _logger;

    public MaterialTypeService(IProductsLookupDataService<MaterialCategory> categoryService, HttpService httpService, ILogger<MaterialTypeService> logger)
    {
        _categoryService = categoryService;
        _httpService = httpService;
        _logger = logger;
    }

    public async Task<MaterialType> Create(CreateMaterialTypeRequest materialType)
    {
        var response = await _httpService.HttpPost<CreateMaterialTypeResponse>("material-types", materialType);
        return response?.MaterialType;
    }

    public async Task<MaterialType> Edit(MaterialType materialType)
    {
        return (await _httpService.HttpPut<EditMaterialTypeResult>("material-types", materialType)).MaterialType;
    }

    public async Task<string> Delete(int materialTypeId)
    {
        return (await _httpService.HttpDelete<DeleteCatalogItemResponse>("material-types", materialTypeId)).Status;
    }

    public async Task<MaterialType> GetById(int id)
    {
        var categoryListTask = _categoryService.List();
        var materialTypeGetTask = _httpService.HttpGet<EditMaterialTypeResult>($"material-types/{id}");
        await Task.WhenAll(categoryListTask, materialTypeGetTask);
        var categories = categoryListTask.Result;
        var materialType = materialTypeGetTask.Result.MaterialType;
        materialType.MaterialCategory = categories.FirstOrDefault(t => t.Id == materialType.MaterialCategoryId)?.Name;
        return materialType;    
    }

    public async Task<List<MaterialType>> ListPaged(int pageSize)
    {
        _logger.LogInformation("Fetching material types from API.");

        var categoryListTask = _categoryService.List();
        var materialTypeListTask = _httpService.HttpGet<PagedMaterialTypeResponse>($"material-types?PageSize=10");
        await Task.WhenAll(categoryListTask, materialTypeListTask);
        var categories = categoryListTask.Result;
        var materialTypes = materialTypeListTask.Result.MaterialTypes;
        foreach (var materialType in materialTypes)
        {
            materialType.MaterialCategory = categories.FirstOrDefault(t => t.Id == materialType.MaterialCategoryId)?.Name;
        }
        return materialTypes;    
    }

    public async Task<List<MaterialType>> List()
    {
        _logger.LogInformation("Fetching material types from API.");

        var categoryListTask = _categoryService.List();
        var materialTypeListTask = _httpService.HttpGet<PagedMaterialTypeResponse>($"material-types");
        await Task.WhenAll(categoryListTask, materialTypeListTask);
        var categories = categoryListTask.Result;
        var materialTypes = materialTypeListTask.Result.MaterialTypes;
        foreach (var materialType in materialTypes)
        {
            materialType.MaterialCategory = categories.FirstOrDefault(t => t.Id == materialType.MaterialCategoryId)?.Name;
        }
        return materialTypes;    
    }
}
