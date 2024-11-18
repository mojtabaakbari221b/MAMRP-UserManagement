namespace UserManagement.Application.ApplicationServices.Roles.Queries.GetAll;

public sealed record GetAllRoleQueryRequest(
    PaginationFilter Pagination,
    RoleFiltering? Filtering,
    RoleOrdering? Ordering)
    : IRequest<PaginationResult<IEnumerable<GetRoleQueryResponse>>>;