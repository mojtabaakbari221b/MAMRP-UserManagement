namespace UserManagement.Application.ApplicationServices.Sections.Commands.Update;

public class UpdateSectionCommandHandler(IUnitOfWork uow) : IRequestHandler<UpdateSectionCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(UpdateSectionCommandRequest request, CancellationToken token)
    {
        var section = await _uow.Sections.FindAsync(request.Id, token)
                      ?? throw new InvalidOperationException();

        section.Name = request.Name;
        section.Description = request.Description;
        section.Url = request.Url;
        section.GroupId = request.GroupId;

        _uow.Sections.Update(section);
        await _uow.SaveChangesAsync(token);
    }
}