namespace UserManagement.Application.ApplicationServices.Account.Queries.GetAll;

public sealed class GetAllUserQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllUserQueryRequest, PaginationResult<IEnumerable<GetUserQueryResponse>>>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<PaginationResult<IEnumerable<GetUserQueryResponse>>> Handle(GetAllUserQueryRequest request, CancellationToken token)
    {
        var results = await _uow.Users
            .GetAll(request.Pagination, request.Filtering, request.Ordering, token);
        
       
        return new PaginationResult<IEnumerable<GetUserQueryResponse>>
        (
            data: results.Responses.Adapt<IEnumerable<GetUserQueryResponse>>(),
            pageNumber: request.Pagination.PageNumber,
            pageSize: request.Pagination.PageSize,
            totalRecords: results.Count
        );
    }
}