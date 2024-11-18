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

    [HttpPost("LoginByRefreshToken")]
    [Authorize(AuthenticationSchemes = "RefershSchema")]
    public async Task<SuccessResponse<LoginCommandResponse>> LoginByRefreshToken(CancellationToken token = default)
    {
        var result = await _sender.Send(new LoginByRefreshTokenCommandRequest(User.UserId()), token);
        return Result.Ok(result);
    }

    [HttpPut("{userId:guid:required}")]
    [Authorize(Policy = ServiceDeclaration.UpdateUser)]
    public async Task<SuccessResponse> UpdateUser(Guid userId, UpdateUserDto model,
        CancellationToken token = default)
    {
        var request = UpdateUserCommandRequest.Create(userId, model);
        await _sender.Send(request, token);
        return Result.Ok();
    }

    [HttpDelete("{userId:guid:required}")]
    [Authorize(Policy = ServiceDeclaration.DeleteUser)]
    public async Task<SuccessResponse> DeleteUser(Guid userId,
        CancellationToken token = default)
    {
        await _sender.Send(new DeleteUserCommandRequest(userId), token);
        return Result.Ok();
    }

    [HttpPut("{userId:guid:required}/UserRoles/{roleId:guid:required}")]
    [Authorize(Policy = ServiceDeclaration.ChangeUserRole)]
    public async Task<SuccessResponse> ChangeUserRole(Guid userId, Guid roleId)
    {
        await _sender.Send(new ChangeRoleOfUserRequest(userId.ToString(), roleId.ToString()));
        return Result.Ok();
    }


    [HttpPut("{userId:guid:required}/CLaims")]
    [Authorize(Policy = ServiceDeclaration.ChangeUserSectionClaim)]
    public async Task<SuccessResponse> ChangeUserSectionClaim(Guid userId, List<long> selectionIds,
        CancellationToken token = default)
    {
        await _sender.Send(new ChangeSectionClaimOfUserRequest(userId, selectionIds), token);
        return Result.Ok();
    }

    [HttpGet("{userId:guid:required}")]
    public async Task<SuccessResponse<GetUserQueryResponse>> GetUserById(Guid userId,
        CancellationToken token = default)
    {
        var result = await _sender.Send(new GetUserByIdQueryRequest(userId), token);
        return Result.Ok(result);
    }

    [HttpGet]
    public async Task<SuccessResponse<PaginationResult<IEnumerable<GetUserQueryResponse>>>> GetAllUser(
        [FromQuery] GetAllUserQueryRequest request,
        CancellationToken token = default)
    {
        var results = await _sender.Send(request, token);
        return Result.Ok(results);
    }
}