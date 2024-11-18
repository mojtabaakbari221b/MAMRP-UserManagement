namespace UserManagement.Application.ApplicationServices.Menus.Commands.Delete;

public sealed class DeleteMenuCommandHandler(IUnitOfWork uow) : IRequestHandler<DeleteMenuCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(DeleteMenuCommandRequest request, CancellationToken token)
    {
        var menu = await _uow.Sections.FindAsync(request.Id, token)
                   ?? throw new MenuNotFoundException();
        
        _uow.Sections.Delete(menu);
        await _uow.SaveChangesAsync(token);
    }
}