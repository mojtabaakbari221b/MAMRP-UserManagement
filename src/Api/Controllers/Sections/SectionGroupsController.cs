namespace UserManagement.Api.Controllers.Sections;



[ApiController]
[Route("api/[controller]")]
public class SectionGroupsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<Result<SectionGroupDto>> Create(AddSectionGroupCommandRequest request,
        CancellationToken token = default)
    {
        var result = await _sender.Send(request, token);
        return Result.Ok(result);
    }

    [HttpPut("{id:long:required}")]
    public async Task<Result> Update(long id, [FromBody] string name,
        CancellationToken token = default)
    {
        await _sender.Send(new UpdateSectionGroupCommandRequest(id, name), token);
        return Result.Ok();
    }


    [HttpDelete("{id:long:required}")]
    public async Task<Result> Delete(long id,
        CancellationToken token = default)
    {
        await _sender.Send(new DeleteSectionGroupCommandRequest(id), token);
        return Result.Ok();
    }

    [HttpGet("{id:long:required}")]
    public async Task<Result<GetSectionGroupByIdQueryResponse>> GetById(long id, CancellationToken token = default)
    {
        var result = await _sender.Send(new GetSectionGroupByIdQueryRequest(id), token);
        return Result.Ok(result);
    }

    [HttpGet]
    public async Task<Result<IEnumerable<GetAllSectionGroupQueryResponse>>> GetAll(
        [FromQuery] GetAllSectionGroupQueryRequest request,
        CancellationToken token = default)
    {
        var result = await _sender.Send(request, token);
        return Result.Ok(result);
    }
}