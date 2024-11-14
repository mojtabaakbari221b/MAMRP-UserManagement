namespace UserManagement.Application.ApplicationServices.Services.Commands.Add;

public class AddServiceCommandHandler(IUnitOfWork uow) : IRequestHandler<AddServiceCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;
    public async Task Handle(AddServiceCommandRequest request, CancellationToken token)
    {
        if (await _uow.Sections.AnyAsync(request.Code, SectionType.Service, token))
        {
            throw new ServiceAlreadyExistException();
        }

        Section section = new()
        {
            Code = request.Code,
            Description = request.Description,
            Name = request.Name,
            DisplayName = request.DisplayName,
            Url = request.Url,
            Type = SectionType.Service,
            GroupId = request.GroupId,
        };
        await _uow.Sections.AddAsync(section);
        await _uow.SaveChangesAsync(token);
    }
}