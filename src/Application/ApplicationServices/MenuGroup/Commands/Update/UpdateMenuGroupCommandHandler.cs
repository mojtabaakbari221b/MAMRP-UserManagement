namespace UserManagement.Application.ApplicationServices.MenuGroup.Commands.Update;


public sealed class UpdateMenuGroupCommandHandler(IUnitOfWork uow)
    : IRequestHandler<UpdateMenuGroupCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(UpdateMenuGroupCommandRequest request, CancellationToken token)
    {
        var sectionGroup = await _uow.SectionGroups.FindAsync(request.Id, SectionType.Menu, token)
                           ?? throw new ServiceGroupNotFoundException();

        sectionGroup.Name = request.Name;
        
        _uow.SectionGroups.Update(sectionGroup);
        await _uow.SaveChangesAsync(token);
    }
}