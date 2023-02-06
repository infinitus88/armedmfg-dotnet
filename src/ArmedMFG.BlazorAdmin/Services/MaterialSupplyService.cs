using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class MaterialSupplyService : IMaterialSupplyService
{
    private readonly IMaterialTypeService _materialTypeService;
    private readonly HttpService _httpService;
    private readonly ILogger<MaterialSupplyService> _logger;

    public MaterialSupplyService(HttpService httpService, ILogger<MaterialSupplyService> logger, IMaterialTypeService materialTypeService)
    {
        _httpService = httpService;
        _logger = logger;
        _materialTypeService = materialTypeService;
    }


    public async Task<MaterialSupply> Create(CreateMaterialSupplyRequest materialSupply)
    {
        var response = await _httpService.HttpPost<CreateMaterialSupplyResponse>("material-types/supplies", materialSupply);
        return response?.MaterialSupply;
    }

    public async Task<MaterialSupply> Edit(MaterialSupply materialSupply)
    {
        return (await _httpService.HttpPut<EditMaterialSupplyResult>("material-types/supplies", materialSupply)).MaterialSupply;
    }

    public async Task<string> Delete(int materialSupplyId)
    {
        return (await _httpService.HttpDelete<DeleteMaterialSupplyResponse>("material-types/supplies", materialSupplyId)).Status;
    }

    public async Task<MaterialSupply> GetById(int id)
    {
        var materialTypeListTask = _materialTypeService.List();
        var materialSupplyGetTask = _httpService.HttpGet<EditMaterialSupplyResult>($"material-types/supplies/{id}");
        await Task.WhenAll(materialTypeListTask, materialSupplyGetTask);
        
        var materialTypes = materialTypeListTask.Result;
        var materialSupply = materialSupplyGetTask.Result.MaterialSupply;
        
        materialSupply.MaterialType = materialTypes.FirstOrDefault(t => t.Id == materialSupply.MaterialTypeId)?.Name;
        
        return materialSupply;
    }

    public async Task<List<MaterialSupply>> ListPaged(int pageSize, int? materialTypeId)
    {
        _logger.LogInformation("Fetching material supplies from API.");

        var materialTypeListTask = _materialTypeService.List();
        var materialSupplyListTask = _httpService.HttpGet<PagedMaterialSupplyResponse>($"material-types/supplies?PageSize=10&MaterialTypeId={materialTypeId}");
        await Task.WhenAll(materialTypeListTask, materialSupplyListTask);
        
        var materialTypes = materialTypeListTask.Result;
        var materialSupplies = materialSupplyListTask.Result.MaterialSupplies;

        foreach (var materialSupply in materialSupplies)
        {
            materialSupply.MaterialType = materialTypes.FirstOrDefault(t => t.Id == materialSupply.MaterialTypeId)?.Name;
        }

        return materialSupplies;
    }

    public async Task<List<MaterialSupply>> List()
    {
        _logger.LogInformation("Fetching material supplies from API.");

        var materialTypeListTask = _materialTypeService.List();
        var materialSupplyListTask = _httpService.HttpGet<PagedMaterialSupplyResponse>($"material-types/supplies");
        await Task.WhenAll(materialTypeListTask, materialSupplyListTask);
        
        var materialTypes = materialTypeListTask.Result;
        var materialSupplies = materialSupplyListTask.Result.MaterialSupplies;

        foreach (var materialSupply in materialSupplies)
        {
            materialSupply.MaterialType = materialTypes.FirstOrDefault(t => t.Id == materialSupply.MaterialTypeId)?.Name;
        }

        return materialSupplies;
    }
}
