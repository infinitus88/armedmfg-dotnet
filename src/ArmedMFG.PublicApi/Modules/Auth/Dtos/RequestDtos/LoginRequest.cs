namespace ArmedMFG.PublicApi.Modules.Auth.Dtos.RequestDtos;

public class LoginRequest : BaseRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
