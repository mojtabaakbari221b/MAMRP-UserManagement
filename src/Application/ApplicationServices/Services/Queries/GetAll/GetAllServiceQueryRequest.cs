namespace UserManagement.Application.ApplicationServices.Services.Queries.GetAll;

public record GetAllServiceQueryRequest(int PageNumber, int PageSize) 
    : IRequest<PaginationResult<IEnumerable<ServiceDto>>>;