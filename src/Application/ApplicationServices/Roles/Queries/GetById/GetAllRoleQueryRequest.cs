using Share.QueryFilterings;
using UserManagement.Domain.Filterings;

namespace UserManagement.Application.ApplicationServices.Roles.Queries.GetById;

public sealed record GetAllRoleQueryRequest(PaginationFilter Pagination, RoleFiltering Filtering)
    : IRequest<IEnumerable<GetRoleQueryResponse>>;