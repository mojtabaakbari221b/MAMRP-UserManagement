namespace UserManagement.Application.ApplicationServices.Services.Queries.GetById;


public record GetSectionByIdQueryRequest(long Id) : IRequest<ServiceDto>;
