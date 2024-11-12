using Share.Helper;
using UserManagement.Application.ApplicationServices.UserRoles.Commands.ChangeSectionClaimOfRole;
using UserManagement.Application.ApplicationServices.UserRoles.Commands.ChangeRoleOfUser;

namespace UserManagement.Api.Controllers.Account;


[ApiController]
[Route("api/[controller]")]
public sealed class AccountController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost("register")]
    [Authorize(Policy = ServiceDeclaration.Register)]
    public async Task<Result> Register(RegisterCommandRequest request,
        CancellationToken token = default)
    {
        await _sender.Send(request, token);
        return Result.Ok();
    }
    
    [HttpPost("login")]
    [Authorize(Policy = ServiceDeclaration.Login)]
    public async Task<Result<LoginQueryResponse>> Login(LoginQueryRequest request,
        CancellationToken token = default)
    {
        var result = await _sender.Send(request, token);
        return Result.Ok(result);
    }

    [HttpPost("change-user-role")]
    [Authorize(Policy = ServiceDeclaration.ChangeUserRole)]
    public async Task<Result> ChangeUserRole(ChangeRoleOfUserRequest request) {
        await _sender.Send(request);
        return Result.Ok();
    }
    
    [HttpPost("change-role-section-claim")]
    [Authorize(Policy = ServiceDeclaration.ChangeRoleSectionClaim)]
    public async Task<Result> ChangeRoleSectionClaim(ChangeSectionClaimOfRoleRequest request) {
        await _sender.Send(request);
        return Result.Ok();
    }
}