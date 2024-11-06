namespace UserManagement.Application.ApplicationServices.SectionGroups.Commands.Delete;

public sealed record DeleteSectionGroupCommandRequest(long Id) : IRequest;

public sealed class DeleteSectionGroupCommandHandler(IUnitOfWork uow)
    : IRequestHandler<DeleteSectionGroupCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(DeleteSectionGroupCommandRequest request, CancellationToken token)
    {
        var sectionGroup = await _uow.SectionGroups.FindAsync(request.Id, token)
                           ?? throw new SectionGroupNotFoundException();

        _uow.SectionGroups.Delete(sectionGroup);
        await _uow.SaveChangeAsync(token);
    }
}