using UserManagement.Domain.Filterings;
using UserManagement.Domain.Orderings;

namespace UserManagement.Application.ApplicationServices.MenuGroup.Queries.GetAll;

public record GetAllMenuGroupQueryRequest(
    PaginationFilter Pagination,
    MenuGroupFiltering? Filtring,
    MenuGroupOrdering? Ordering)
    : IRequest<PaginationResult<IEnumerable<SectionGroupDto>>>;