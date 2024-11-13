namespace UserManagement.Application.ApplicationServices.Menus.Queries.GetAll;

public sealed class GetAllMenuQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllMenuQueryRequest, IEnumerable<MenuDto>>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<IEnumerable<MenuDto>> Handle(GetAllMenuQueryRequest request, CancellationToken token)
    {
        var responses = await _uow.Sections.GetAll(request.Pagination, request.Filtering, SectionType.Menu, token);
        return responses.Adapt<IEnumerable<MenuDto>>();
    }
}