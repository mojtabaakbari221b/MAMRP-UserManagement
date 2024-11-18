namespace UserManagement.Application.ApplicationServices.Menus.Queries.GetAll;

public sealed class GetAllMenuQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllMenuQueryRequest, PaginationResult<IEnumerable<MenuDto>>>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<PaginationResult<IEnumerable<MenuDto>>> Handle(GetAllMenuQueryRequest request, CancellationToken token)
    {
        var results = await _uow.Sections.GetAll(request.Pagination, request.Filtering, request.Ordering, SectionType.Menu, token);
        
        return new PaginationResult<IEnumerable<MenuDto>>
        (
            data: results.Responses.Adapt<IEnumerable<MenuDto>>(),
            pageNumber: request.Pagination.PageNumber,
            pageSize: request.Pagination.PageSize,
            totalRecords: results.Count
        );
    }
}