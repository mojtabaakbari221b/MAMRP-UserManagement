namespace UserManagement.Application.ApplicationServices.MenuGroup.Commands.Add;

public sealed class AddMenuGroupCommandHandler(IUnitOfWork uow)
    : IRequestHandler<AddMenuGrcoupCommandRequest, SectionGroupDto>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<SectionGroupDto> Handle(AddMenuGrcoupCommandRequest request, CancellationToken token)
    {
        SectionGroup newSectionGroup = new()
        {
            Name = request.Name,
            Type = SectionType.Menu
        };
        
        await _uow.SectionGroups.AddAsync(newSectionGroup, token);
        await _uow.SaveChangesAsync(token);
        return newSectionGroup.Adapt<SectionGroupDto>();
    }
}