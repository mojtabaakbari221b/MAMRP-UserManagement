using Share.QueryFilterings;

namespace UserManagement.Api.Controllers.Sections;


[ApiController]
[Route("api/[controller]")]
public sealed class ServicesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    [Authorize(Policy = ServiceDeclaration.GetAllSections)]
    public async Task<Result<PaginationResult<IEnumerable<ServiceDto>>>> GetAll([FromQuery] GetAllServiceQueryRequest request, CancellationToken token = default)
    {
        var responses = await _sender.Send(request, token);
        return Result.Ok(responses);
    }

    [HttpGet("{id:long:required}")]
    [Authorize(Policy = ServiceDeclaration.GetByIdSection)]
    public async Task<Result<ServiceDto>> GetById(long id,
        CancellationToken token = default)
    {
        var result = await _sender.Send(new GetSectionByIdQueryRequest(id), token);
        return Result.Ok(result);
    }

    [HttpDelete("{id:long:required}")]
    [Authorize(Policy = ServiceDeclaration.DeleteSection)]
    public async Task<Result<bool>> Delete(long id,
        CancellationToken token = default)
    {
        await _sender.Send(new DeleteServiecCommandRequest(id), token);
        return Result.Ok(true);
    }

    [HttpPut("{id:long:required}")]
    [Authorize(Policy = ServiceDeclaration.UpdateSection)]
    public async Task<Result<bool>> Update(long id, UpdateServiceDto model,
        CancellationToken token = default)
    {
        var command = UpdateServiceCommandRequest.Create(id, model);
        await _sender.Send(command, token);
        return Result.Ok(true);
    }
}