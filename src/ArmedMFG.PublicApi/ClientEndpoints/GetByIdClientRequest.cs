namespace ArmedMFG.PublicApi.ClientEndpoints;

public class GetByIdClientRequest : BaseRequest
{
    public int ClientId { get; init; }

    public GetByIdClientRequest(int clientId)
    {
        ClientId = clientId;
    }
}
