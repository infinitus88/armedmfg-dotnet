using System;

namespace ArmedMFG.PublicApi.Modules.Auth.Dtos.ResponseDtos;

public class LoginResponse : BaseResponse
{
    public LoginResponse(Guid correlationId) : base(correlationId)
    {
    }

    public LoginResponse()
    {
    }
    public bool Result { get; set; } = false;
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public bool IsLockedOut { get; set; } = false;
    public bool IsNotAllowed { get; set; } = false;
    public bool RequiresTwoFactor { get; set; } = false;
}
