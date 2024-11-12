namespace UserManagement.Application.ApplicationServices.Menus.Queries.GetAll;

public sealed class GetAllMenuQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllMenuQueryRequest, IEnumerable<MenuDto>>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<IEnumerable<MenuDto>> Handle(GetAllMenuQueryRequest request, CancellationToken token)
    {
        var responses = await _uow.Sections.GetAllMenus(request.PageNumber, request.PageSize, token);
        return responses.Adapt<IEnumerable<MenuDto>>();
    }
}