namespace UserManagement.Application.ApplicationServices.Services.Queries.GetAll;

public record GetAllServiceQueryRequest : IRequest<IEnumerable<ServiceDto>>;