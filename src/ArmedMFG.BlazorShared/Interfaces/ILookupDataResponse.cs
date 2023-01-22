using System.Collections.Generic;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorShared.Interfaces;

public interface ILookupDataResponse<TLookupData> where TLookupData : LookupData
{
    List<TLookupData> List { get; set; }
}
