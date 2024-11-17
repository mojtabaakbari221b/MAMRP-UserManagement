namespace UserManagement.Application.ApplicationServices.MenuGroup.Queries.GetById;

public sealed record GetMenuGroupByIdQueryRequest(long Id) : IRequest<SectionGroupDto>;