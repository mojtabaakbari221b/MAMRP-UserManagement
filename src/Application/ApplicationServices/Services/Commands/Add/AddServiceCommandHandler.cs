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

        var section = request.Adapt<Section>();
        await _uow.Sections.AddAsync(section);
    }
}