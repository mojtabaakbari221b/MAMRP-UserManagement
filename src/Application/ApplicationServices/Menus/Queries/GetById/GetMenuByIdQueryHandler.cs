namespace UserManagement.Application.ApplicationServices.Menus.Queries.GetById;

public sealed class GetMenuByIdQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetMenuByIdQueryRequest, MenuDto>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<MenuDto> Handle(GetMenuByIdQueryRequest request, CancellationToken token)
    {
        var response = await _uow.Sections.GetById(request.Id, SectionType.Menu, token)
                       ?? throw new MenuNotFoundException();

        return response.Adapt<MenuDto>();
    }
}