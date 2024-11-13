namespace UserManagement.Application.ApplicationServices.Services.Commands.Delete;

public class DeleteServiceCommandHandler(IUnitOfWork uow) : IRequestHandler<DeleteServiecCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(DeleteServiecCommandRequest request, CancellationToken token)
    {
        var section = await _uow.Sections.FindAsync(request.Id, token) 
                      ?? throw new ServiceNotFoundException();

        _uow.Sections.Delete(section);
        await _uow.SaveChangesAsync(token);
    }
}