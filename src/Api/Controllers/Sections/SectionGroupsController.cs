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
}