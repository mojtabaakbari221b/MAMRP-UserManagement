namespace UserManagement.Application.ApplicationServices.SectionGroups.Commands.Add;

public sealed class AddSectionGroupCommandHandler(IUnitOfWork uow)
    : IRequestHandler<AddSectionGroupCommandRequest, SectionGroupDto>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<SectionGroupDto> Handle(AddSectionGroupCommandRequest request, CancellationToken token)
    {
        var newSectionGroup = request.Adapt<SectionGroup>();
        var sectionGroup = await _uow.SectionGroups.AddAsync(newSectionGroup, token);
        await _uow.SaveChangeAsync(token);
        return sectionGroup.Adapt<SectionGroupDto>();
    }
}