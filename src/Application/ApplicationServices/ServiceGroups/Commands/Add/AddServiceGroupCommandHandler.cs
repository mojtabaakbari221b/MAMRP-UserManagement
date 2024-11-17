namespace UserManagement.Application.ApplicationServices.ServiceGroups.Commands.Add;

public sealed class AddServiceGroupCommandHandler(IUnitOfWork uow)
    : IRequestHandler<AddServiceGroupCommandRequest, SectionGroupDto>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<SectionGroupDto> Handle(AddServiceGroupCommandRequest request, CancellationToken token)
    {
        SectionGroup newSectionGroup = new()
        {
            Name = request.Name,
            Type = SectionType.Service
        };

        await _uow.SectionGroups.AddAsync(newSectionGroup, token);
        await _uow.SaveChangesAsync(token);
        return newSectionGroup.Adapt<SectionGroupDto>();
    }
}