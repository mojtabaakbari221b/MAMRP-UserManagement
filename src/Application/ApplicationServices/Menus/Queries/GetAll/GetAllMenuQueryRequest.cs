using Share.QueryFilterings;
using UserManagement.Domain.Filterings;
using UserManagement.Domain.Orderings;

namespace UserManagement.Application.ApplicationServices.Menus.Queries.GetAll;

public sealed record GetAllMenuQueryRequest(
    PaginationFilter Pagination, MenuFiltering? Filtering, MenuOrdering? Ordering
    ) 
    : IRequest<PaginationResult<IEnumerable<MenuDto>>>;