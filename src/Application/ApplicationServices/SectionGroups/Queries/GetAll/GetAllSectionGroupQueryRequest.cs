namespace UserManagement.Application.ApplicationServices.SectionGroups.Queries.GetAll;

public record GetAllSectionGroupQueryRequest(int PageNumber, int PageSize, SectionType Type)
    : IRequest<IEnumerable<GetAllSectionGroupQueryResponse>>;