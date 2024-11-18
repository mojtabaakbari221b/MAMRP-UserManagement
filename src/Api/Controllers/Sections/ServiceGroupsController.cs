namespace UserManagement.Api.Controllers.Sections;

[ApiController]
[Route("api/[controller]")]
public sealed class ServiceGroupsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<SuccessResponse<SectionGroupDto>> Create(AddServiceGroupCommandRequest request,
        CancellationToken token = default)
    {
        var result = await _sender.Send(request, token);
        return Result.Ok(result);
    }

    [HttpPut("{id:long:required}")]
    public async Task<SuccessResponse> Update(long id, [FromBody] string name,
        CancellationToken token = default)
    {
        await _sender.Send(new UpdateServiceGroupCommandRequest(id, name), token);
        return Result.Ok();
    }


    [HttpDelete("{id:long:required}")]
    public async Task<SuccessResponse> Delete(long id,
        CancellationToken token = default)
    {
        await _sender.Send(new DeleteServiceGroupCommandRequest(id), token);
        return Result.Ok();
    }

    [HttpGet("{id:long:required}")]
    public async Task<SuccessResponse<SectionGroupDto>> GetById(long id, CancellationToken token = default)
    {
        var result = await _sender.Send(new GetServiceGroupByIdQueryRequest(id), token);
        return Result.Ok(result);
    }

    [HttpGet]
    public async Task<SuccessResponse<PaginationResult<IEnumerable<SectionGroupDto>>>> GetAll(
        [FromQuery] GetAllServiceGroupQueryRequest request,
        CancellationToken token = default)
    {
        var result = await _sender.Send(request, token);
        return Result.Ok(result);
    }
}