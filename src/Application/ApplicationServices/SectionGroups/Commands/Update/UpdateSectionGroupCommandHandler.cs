namespace UserManagement.Application.ApplicationServices.SectionGroups.Commands.Update;


public sealed class UpdateSectionGroupCommandHandler(IUnitOfWork uow)
    : IRequestHandler<UpdateSectionGroupCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(UpdateSectionGroupCommandRequest request, CancellationToken token)
    {
        var sectionGroup = await _uow.SectionGroups.FindAsync(request.Id, token)
                           ?? throw new SectionGroupNotFoundException();

        sectionGroup.Name = request.Name;
        
        _uow.SectionGroups.Update(sectionGroup);
        await _uow.SaveChangesAsync(token);
    }
}