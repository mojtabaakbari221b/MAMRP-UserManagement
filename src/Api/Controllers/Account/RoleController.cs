namespace UserManagement.Api.Controllers.Account;

[ApiController]
[Route("api/[controller]")]
public sealed class RoleController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<Result<AddRoleCommandResponse>> Post(AddRoleCommandRequest request,
        CancellationToken token = default)
    {
        var response = await _sender.Send(request, token);
        return Result.Ok(response);
    }

    [HttpPut("{id:guid:required}")]
    public async Task<Result> Put(Guid id, UpdateRoleDto model,
        CancellationToken token = default)
    {
        var request = UpdateRoleCommandRequest.Create(id, model);
        await _sender.Send(request, token);
        return Result.Ok();
    }

    [HttpDelete("{id:guid:required}")]
    public async Task<Result> Delete(Guid id,
        CancellationToken token = default)
    {
        await _sender.Send(new DeleteRoleCommandRequest(id), token);
        return Result.Ok();
    }
    
    [HttpGet]
    public async Task<Result<IEnumerable<GetRoleQueryResponse>>> GetAll([FromQuery] GetAllRoleQueryRequest request, 
        CancellationToken token = default)
    {
        var response = await _sender.Send(request, token);
        return Result.Ok(response);
    }
    
    [HttpGet("{id:guid:required}")]
    public async Task<Result<GetRoleQueryResponse>> Get(Guid id,
        CancellationToken token = default)
    {
        var response = await _sender.Send(new GetRoleByIdQueryReqeust(id), token);
        return Result.Ok(response);
    }
}