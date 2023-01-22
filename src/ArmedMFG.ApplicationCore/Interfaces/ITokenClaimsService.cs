using System.Threading.Tasks;

namespace ArmedMFG.ApplicationCore.Interfaces;

public interface ITokenClaimsService
{
    Task<string> GetTokenAsync(string userName);
}
