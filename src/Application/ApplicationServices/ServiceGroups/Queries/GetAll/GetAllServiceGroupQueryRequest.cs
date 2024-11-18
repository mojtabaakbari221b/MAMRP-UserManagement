using UserManagement.Domain.Filterings;
using UserManagement.Domain.Orderings;

namespace UserManagement.Application.ApplicationServices.ServiceGroups.Queries.GetAll;

public record GetAllServiceGroupQueryRequest(
    PaginationFilter Pagination,
    ServiceGroupFiltring? Filtring,
    ServiceGroupOrdering? Ordering)
    : IRequest<PaginationResult<IEnumerable<SectionGroupDto>>>;