namespace UserManagement.Application.ApplicationServices.ServiceGroups.Queries.GetAll;

public record GetAllServiceGroupQueryRequest(int PageNumber, int PageSize, SectionType Type)
    : IRequest<IEnumerable<SectionGroupDto>>;