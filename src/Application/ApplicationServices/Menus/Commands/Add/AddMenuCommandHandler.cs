namespace UserManagement.Application.ApplicationServices.Menus.Commands.Add;

public sealed class AddMenuCommandHandler(IUnitOfWork uow) : IRequestHandler<AddMenuCommandReqeust, MenuDto>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<MenuDto> Handle(AddMenuCommandReqeust request, CancellationToken token)
    {
        if (!await _uow.SectionGroups.AnyAsync(request.GroupId, token))
        {
            throw new SectionGroupNotFoundException();
        }

        Section menu = new()
        {
            Name = request.Name,
            DisplayName = request.DisplayName,
            Description = request.Description,
            Url = request.Url,
            GroupId = request.GroupId,
            Type = SectionType.Menu
        };

        await _uow.Sections.AddAsync(menu);
        await _uow.SaveChangesAsync(token);

        return menu.Adapt<MenuDto>();
    }
}