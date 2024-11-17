namespace UserManagement.Application.ApplicationServices.ServiceGroups.Queries.GetById;

public sealed record GetServiceGroupByIdQueryRequest(long Id) : IRequest<SectionGroupDto>;