namespace UserManagement.Application.ApplicationServices.Sections.Commands.Add;

public class AddSectionCommandHandler(IUnitOfWork uow) : IRequestHandler<AddSectionCommandRequest, SectionDto>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<SectionDto> Handle(AddSectionCommandRequest request, CancellationToken token)
    {
        if (!await _uow.SectionGroups.AnyAsync(request.GroupId, token))
        {
            throw new SectionGroupNotFoundException();
        }

        var newSection = request.Adapt<Section>();
        var section = await _uow.Sections.Add(newSection);
        await _uow.SaveChangeAsync(token);
        return section.Adapt<SectionDto>();
    }
}