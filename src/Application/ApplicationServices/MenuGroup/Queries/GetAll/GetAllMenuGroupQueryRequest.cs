namespace UserManagement.Application.ApplicationServices.MenuGroup.Queries.GetAll;

public record GetAllMenuGroupQueryRequest(int PageNumber, int PageSize, SectionType Type)
    : IRequest<IEnumerable<SectionGroupDto>>;