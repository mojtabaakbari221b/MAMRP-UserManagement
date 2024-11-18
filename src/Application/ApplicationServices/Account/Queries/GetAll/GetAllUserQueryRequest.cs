namespace UserManagement.Application.ApplicationServices.Account.Queries.GetAll;

public sealed record GetAllUserQueryRequest(
    PaginationFilter Pagination,
    UserFiltering Filtering,
    UserOrdering Ordering)
    : IRequest<PaginationResult<IEnumerable<GetUserQueryResponse>>>;