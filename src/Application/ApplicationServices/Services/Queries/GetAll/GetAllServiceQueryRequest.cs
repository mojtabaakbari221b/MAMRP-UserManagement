using Share.QueryFilterings;
using UserManagement.Domain.Filterings;

namespace UserManagement.Application.ApplicationServices.Services.Queries.GetAll;

public record GetAllServiceQueryRequest(PaginationFilter Pagination, ServiceFiltering? Filtering) 
    : IRequest<PaginationResult<IEnumerable<ServiceDto>>>;