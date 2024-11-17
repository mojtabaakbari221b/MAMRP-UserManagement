namespace UserManagement.Application.ApplicationServices.ServiceGroups.Commands.Update;


public sealed class UpdateServiceGroupCommandHandler(IUnitOfWork uow)
    : IRequestHandler<UpdateServiceGroupCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(UpdateServiceGroupCommandRequest request, CancellationToken token)
    {
        var sectionGroup = await _uow.SectionGroups.FindAsync(request.Id, SectionType.Service, token)
                           ?? throw new SectionGroupNotFoundException();

        sectionGroup.Name = request.Name;
        
        _uow.SectionGroups.Update(sectionGroup);
        await _uow.SaveChangesAsync(token);
    }
}