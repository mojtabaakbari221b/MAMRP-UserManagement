namespace UserManagement.Application.ApplicationServices.Menus.Commands.Update;

public sealed class UpdateMenuCommandHandler(IUnitOfWork uow)
    : IRequestHandler<UpdateMenuCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(UpdateMenuCommandRequest request, CancellationToken token)
    {
        if (!await _uow.SectionGroups.AnyAsync(request.GroupId, SectionType.Menu, token))
        {
            throw new MenuGroupNotFoundException();
        }

        var menu = await _uow.Sections.FindAsync(request.Id, token)
                   ?? throw new MenuNotFoundException();

        menu.Description = request.Description;
        menu.Name = request.Name;
        menu.Url = request.Url;
        menu.DisplayName = request.DisplayName;
        menu.GroupId = request.GroupId;

        _uow.Sections.Update(menu);
        await _uow.SaveChangesAsync(token);
    }
}