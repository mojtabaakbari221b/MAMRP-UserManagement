using UserManagement.Application.ApplicationServices.UserRoles.Commands.ChangeSectionClaimOfRole;
using UserManagement.Application.ApplicationServices.UserRoles.Commands.ChangeRoleOfUser;

namespace UserManagement.Api.Controllers.Account;


[ApiController]
[Route("api/[controller]")]
public sealed class AccountController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost("register")]
    public async Task<Result> Register(RegisterCommandRequest request,
        CancellationToken token = default)
    {
        await _sender.Send(request, token);
        return Result.Ok();
    }
    
    [HttpGet("login")]
    public async Task<Result<LoginQueryResponse>> Login(LoginQueryRequest request,
        CancellationToken token = default)
    {
        var result = await _sender.Send(request, token);
        return Result.Ok(result);
    }

    [HttpPost("change-user-role")]
    public async Task<Result> ChangeUserRole(ChangeRoleOfUserRequest request) {
        await _sender.Send(request);
        return Result.Ok();
    }
    
    [HttpPost("change-role-section-claim")]
    public async Task<Result> ChangeRoleSectionClaim(ChangeSectionClaimOfRoleRequest request) {
        await _sender.Send(request);
        return Result.Ok();
    }
}