namespace UserManagement.Application.ApplicationServices.Services.Commands.Update;

public class UpdateServiceCommandHandler(IUnitOfWork uow) : IRequestHandler<UpdateServiceCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(UpdateServiceCommandRequest request, CancellationToken token)
    {
        var section = await _uow.Sections.FindAsync(request.Id, token)
                      ?? throw new InvalidOperationException();

        section.DisplayName = request.DisplayName;
        section.Description = request.Description;
        section.Url = request.Url;
        section.GroupId = request.GroupId;

        _uow.Sections.Update(section);
        await _uow.SaveChangesAsync(token);
    }
}