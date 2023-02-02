namespace ArmedMFG.PublicApi.ClientEndpoints;

public class DeleteClientRequest : BaseRequest
{
    public int ClientId { get; set; }

    public DeleteClientRequest(int clientId)
    {
        ClientId = clientId;
    }
}
