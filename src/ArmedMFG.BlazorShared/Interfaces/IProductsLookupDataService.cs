using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorShared.Interfaces;

public interface IProductsLookupDataService<TLookupData> where TLookupData : LookupData
{
    Task<List<TLookupData>> List();
}
