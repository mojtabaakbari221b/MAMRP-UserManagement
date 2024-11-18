namespace UserManagement.Application.ApplicationServices.Roles.Queries.GetAll;

public sealed class GetAllRoleQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllRoleQueryRequest, PaginationResult<IEnumerable<GetRoleQueryResponse>>>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<PaginationResult<IEnumerable<GetRoleQueryResponse>>> Handle(GetAllRoleQueryRequest request,
        CancellationToken token)
    {
        var results = await _uow.Roles
            .GetAll(request.Pagination, request.Filtering, request.Ordering, token);

        return new PaginationResult<IEnumerable<GetRoleQueryResponse>>
        (
            data: results.Responses.Adapt<IEnumerable<GetRoleQueryResponse>>(),
            pageNumber: request.Pagination.PageNumber,
            pageSize: request.Pagination.PageSize,
            totalRecords: results.Count
        );
    }
}