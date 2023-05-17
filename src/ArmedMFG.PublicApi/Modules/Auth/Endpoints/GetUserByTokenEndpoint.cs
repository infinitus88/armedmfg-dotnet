using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ArmedMFG.PublicApi.Modules.Auth.Dtos.ResponseDtos;

namespace ArmedMFG.PublicApi.AuthEndpoints;

public class GetUserByTokenEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<LoginResponse>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenClaimsService _tokenClaimsService;

    public GetUserByTokenEndpoint(SignInManager<ApplicationUser> signInManager, ITokenClaimsService tokenClaimsService)
    {
        _signInManager = signInManager;
        _tokenClaimsService = tokenClaimsService;
    }

    [HttpGet("api/auth/me")]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public override async Task<ActionResult<LoginResponse>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var response = new LoginResponse();
        var result = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        response.Username = result;

        return response;
    }
}

