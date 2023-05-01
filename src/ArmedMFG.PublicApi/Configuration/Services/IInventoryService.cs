namespace ArmedMFG.PublicApi.Configuration.Services;

public interface IProductInventoryService 
{
    InventoryData GetInventoryData(int productTypeId);
}
