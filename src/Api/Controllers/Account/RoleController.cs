namespace UserManagement.Api.Controllers.Account;

[ApiController]
[Route("api/[controller]")]
public sealed class RoleController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    [Authorize(Policy = ServiceDeclaration.CreateRole)]
    public async Task<SuccessResponse<AddRoleCommandResponse>> Post(AddRoleCommandRequest request,
        CancellationToken token = default)
    {
        var response = await _sender.Send(request, token);
        return Result.Ok(response);
    }

    [HttpPut("{id:guid:required}")]
    [Authorize(Policy = ServiceDeclaration.UpdateRole)]
    public async Task<SuccessResponse> Put(Guid id, UpdateRoleDto model,
        CancellationToken token = default)
    {
        var request = UpdateRoleCommandRequest.Create(id, model);
        await _sender.Send(request, token);
        return Result.Ok();
    }

    [HttpDelete("{id:guid:required}")]
    [Authorize(Policy = ServiceDeclaration.DeleteRole)]
    public async Task<SuccessResponse> Delete(Guid id,
        CancellationToken token = default)
    {
        await _sender.Send(new DeleteRoleCommandRequest(id), token);
        return Result.Ok();
    }
    
    [HttpGet]
    [Authorize(Policy = ServiceDeclaration.GetAllRole)]
    public async Task<SuccessResponse<IEnumerable<GetRoleQueryResponse>>> GetAll([FromQuery] GetAllRoleQueryRequest request,
        CancellationToken token = default)
    {
        var response = await _sender.Send(request, token);
        return Result.Ok(response);
    }
    
    [HttpGet("{id:guid:required}")]
    [Authorize(Policy = ServiceDeclaration.GetByIdRole)]
    public async Task<SuccessResponse<GetRoleQueryResponse>> Get(Guid id,
        CancellationToken token = default)
    {
        var response = await _sender.Send(new GetRoleByIdQueryReqeust(id), token);
        return Result.Ok(response);
    }
}