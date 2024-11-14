using Share.QueryFilterings;
using UserManagement.Domain.Filterings;
using UserManagement.Domain.Orderings;

namespace UserManagement.Application.ApplicationServices.Services.Queries.GetAll;

public record GetAllServiceQueryRequest(
    PaginationFilter Pagination, ServiceFiltering? Filtering, ServiceOrdering? Ordering
    ) 
    : IRequest<PaginationResult<IEnumerable<ServiceDto>>>;