using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace ArmedMFG.PublicApi.Configuration.Services;

public class ProductInventoryService : IProductInventoryService
{
    private readonly ConfigFilesSettings _configFilesSettings;
    private readonly Inventory _inventory;

    public ProductInventoryService(IOptions<ConfigFilesSettings> configFilesSettings)
    {
        _configFilesSettings = configFilesSettings.Value;
        
        // Initialize the inventory from the JSON file
        var jsonString = File.ReadAllText(_configFilesSettings.ProductInventoryJsonFilePath);
        _inventory = JsonSerializer.Deserialize<Inventory>(jsonString);
    }
    
    public InventoryData GetInventoryData(int productTypeId)
    {
        // Retrieve the product inventory data for the specified ID
        if (_inventory.Data.TryGetValue(productTypeId, out var productInventoryData))
        {
            return productInventoryData;
        }

        return null;
    }
}
