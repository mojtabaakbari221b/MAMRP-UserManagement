namespace UserManagement.Application.ApplicationServices.MenuGroup.Commands.Delete;

public sealed class DeleteMenuGroupCommandHandler(IUnitOfWork uow)
    : IRequestHandler<DeleteMenuGroupCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(DeleteMenuGroupCommandRequest request, CancellationToken token)
    {
        var sectionGroup = await _uow.SectionGroups.FindAsync(request.Id, SectionType.Menu,  token)
                           ?? throw new SectionGroupNotFoundException();

        _uow.SectionGroups.Delete(sectionGroup);
        await _uow.SaveChangesAsync(token);
    }
}