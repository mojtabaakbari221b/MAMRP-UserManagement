namespace UserManagement.Api.Controllers.Sections;

[ApiController]
[Route("api/[controller]")]
public sealed class MenuController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<SuccessResponse<MenuDto>> Post(AddMenuCommandReqeust reqeust,
        CancellationToken token = default)
    {
        var result = await _sender.Send(reqeust, token);
        return Result.Ok(result);
    }

    [HttpPut("{id:long:required}")]
    public async Task<SuccessResponse> Put(long id, UpdateMenuDto model,
        CancellationToken token = default)
    {
        var reqeust = UpdateMenuCommandRequest.Create(id, model);
        await _sender.Send(reqeust, token);
        return Result.Ok();
    }

    [HttpDelete("{id:long:required}")]
    public async Task<SuccessResponse> Delete(long id, CancellationToken token = default)
    {
        await _sender.Send(new DeleteMenuCommandRequest(id), token);
        return Result.Ok();
    }

    [HttpGet("{id:long:required}")]
    public async Task<SuccessResponse<MenuDto>> Get(long id, CancellationToken token = default)
    {
        var result = await _sender.Send(new GetMenuByIdQueryRequest(id), token);
        return Result.Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAllMenuQueryRequest request,
        CancellationToken token = default)
    {
        var results = await _sender.Send(request, token);
        return Ok(Share.ResponseResult.Result.Ok(results));
    }
}