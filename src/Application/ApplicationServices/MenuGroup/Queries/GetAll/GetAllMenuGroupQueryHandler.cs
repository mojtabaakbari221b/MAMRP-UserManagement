namespace UserManagement.Application.ApplicationServices.MenuGroup.Queries.GetAll;

public sealed class GetAllMenuGroupQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllMenuGroupQueryRequest, PaginationResult<IEnumerable<SectionGroupDto>>>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<PaginationResult<IEnumerable<SectionGroupDto>>> Handle(GetAllMenuGroupQueryRequest request,
        CancellationToken token)
    {
        var results = await _uow.SectionGroups
            .GetAll(request.Pagination, request.Filtring, request.Ordering, SectionType.Menu, token);

        return new PaginationResult<IEnumerable<SectionGroupDto>>
        (
            data: results.Responses.Adapt<IEnumerable<SectionGroupDto>>(),
            pageNumber: request.Pagination.PageNumber,
            pageSize: request.Pagination.PageSize,
            totalRecords: results.Count
        );
    }
}