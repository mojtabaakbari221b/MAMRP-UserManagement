namespace UserManagement.Application.ApplicationServices.ServiceGroups.Commands.Delete;


public sealed class DeleteServiceGroupCommandHandler(IUnitOfWork uow)
    : IRequestHandler<DeleteServiceGroupCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(DeleteServiceGroupCommandRequest request, CancellationToken token)
    {
        var sectionGroup = await _uow.SectionGroups.FindAsync(request.Id, SectionType.Service,token)
                           ?? throw new ServiceGroupNotFoundException();

        _uow.SectionGroups.Delete(sectionGroup);
        await _uow.SaveChangesAsync(token);
    }
}