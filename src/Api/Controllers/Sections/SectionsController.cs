namespace UserManagement.Api.Controllers.Sections;

[ApiController]
[Route("api/[controller]")]
public class SectionsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<Result<IEnumerable<SectionDto>>> GetAll(CancellationToken token = default)
    {
        var responses = await _sender.Send(new GetAllSectionQueryRequest(), token);
        return Result.Ok(responses);
    }

    [HttpGet("{id:long:required}")] 
    public async Task<Result<SectionDto>> GetById(long id,
        CancellationToken token = default)
    {
        var result = await _sender.Send(new GetSectionByIdQueryRequest(id), token);
        return Result.Ok(result);
    }

    [HttpDelete("{id:long:required}")]
    public async Task<Result<bool>> Delete(long id,
        CancellationToken token = default)
    {
        await _sender.Send(new DeleteSectionCommandRequest(id), token);
        return Result.Ok(true);
    }

    [HttpPut("{id:long:required}")]
    public async Task<Result<bool>> Update(long id, UpdateSectionDto model,
        CancellationToken token = default)
    {
        var command = UpdateSectionCommandRequest.Create(id, model);
        await _sender.Send(command, token);
        return Result.Ok(true);
    }

    [HttpPost]
    public async Task<Result<SectionDto>> Create(AddSectionCommandRequest request,
        CancellationToken token = default)
    {
        var result = await _sender.Send(request, token);
        return Result.Ok(result);
    }
}