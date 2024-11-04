using UserManagement.Application.ApplicationServices.SectionGroups.Commands.Add;
using UserManagement.Application.ApplicationServices.Sections.Commands.Add;

namespace UserManagement.Api.Controllers;

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