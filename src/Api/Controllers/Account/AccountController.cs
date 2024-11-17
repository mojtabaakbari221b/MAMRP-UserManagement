namespace UserManagement.Api.Controllers.Account;


[ApiController]
[Route("api/[controller]")]
public sealed class AccountController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost("Register")]
    [Authorize(Policy = ServiceDeclaration.Register)]
    public async Task<SuccessResponse> Register(RegisterCommandRequest request,
        CancellationToken token = default)
    {
        await _sender.Send(request, token);
        return Result.Ok();
    }
    
    [HttpPost("Login")]
    public async Task<SuccessResponse<LoginCommandResponse>> Login([FromBody] LoginCommandRequest request,
        CancellationToken token = default)
    {
        if (! await ReCaptcha.IsValid(request.CaptchaValue))
        {
            throw new ReCaptchaFailedException();
        }
        
        var result = await _sender.Send(request, token);
        return Result.Ok(result);
    }

    [HttpPut]
    [Authorize(Policy = ServiceDeclaration.UpdateUser)]
    public async Task<SuccessResponse> UpdateUser(Guid userId, UpdateUserDto model,
        CancellationToken token = default)
    {
        var request = UpdateUserCommandRequest.Create(userId, model);
        await _sender.Send(request, token);
        return Result.Ok();
    }
    [HttpDelete]
    [Authorize(Policy = ServiceDeclaration.DeleteUser)]
    public async Task<SuccessResponse> DeleteUser(Guid userId, 
        CancellationToken token = default)
    {
        await _sender.Send(new DeleteUserCommandRequest(userId), token);
        return Result.Ok();
    }

    [HttpPost("ChangeUserRole")]
    [Authorize(Policy = ServiceDeclaration.ChangeUserRole)]
    public async Task<SuccessResponse> ChangeUserRole(ChangeRoleOfUserRequest request) {
        await _sender.Send(request);
        return Result.Ok();
    }
        
    
    [HttpPost("ChangeRoleClaim")]
    [Authorize(Policy = ServiceDeclaration.ChangeRoleSectionClaim)]
    public async Task<SuccessResponse> ChangeRoleClaim(ChangeSectionClaimOfRoleRequest request) {
        await _sender.Send(request);
        return Result.Ok();
    }
    
    [HttpPost("ChangeUserClaim")]
    [Authorize(Policy = ServiceDeclaration.ChangeUserSectionClaim)]
    public async Task<SuccessResponse> ChangeUserClaim(ChangeSectionClaimOfRoleRequest request) {
        await _sender.Send(request);
        return Result.Ok();
    }
}