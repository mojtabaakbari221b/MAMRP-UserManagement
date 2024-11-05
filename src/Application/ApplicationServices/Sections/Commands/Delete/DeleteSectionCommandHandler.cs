namespace UserManagement.Application.ApplicationServices.Sections.Commands.Delete;

public class DeleteSectionCommandHandler(IUnitOfWork uow) : IRequestHandler<DeleteSectionCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(DeleteSectionCommandRequest request, CancellationToken token)
    {
        var section = await _uow.Sections.FindAsync(request.Id, token) 
                      ?? throw new SectionNotFoundException();

        _uow.Sections.Delete(section);
        await _uow.SaveChangeAsync(token);
    }
}