namespace UserManagement.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class SectionController(ISender Sender) : ControllerBase
{
    private readonly ISender _sender = Sender;

    [HttpGet]
    public async Task<Result<IList<SectionDto>>> GetAll([FromQuery] GetAllSectionQuery command, CancellationToken token = default)
    {
        var SectionDtos = await _sender.Send(command, token);
        return Result.Ok(SectionDtos);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<Result<SectionDto>> GetById([FromRoute] GetSectionByIdQuery command)
    {
        var menuDto = await _sender.Send(command);
        return Result.Ok(menuDto);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<Result<bool>> Delete([FromRoute] DeleteSectionCommand command)
    {
        var menu = _sender.Send(command);
        return Result.Ok(true);
    }

    //[HttpPut("{id:long}")]
    //public async Task<Result<bool>> Update(long Id, [FromBody] UpdateSectionCommand command)
    //{
    //    command.Id = Id;
    //    await _mediator.Send(command);
    //    return Result.Ok(true);
    //}

    [HttpPost]
    public async Task<Result<SectionDto>> Create([FromBody] AddSectionCommand command) 
    {
        var menuDto = await _sender.Send(command);
        return Result.Ok(menuDto);
    }
}

