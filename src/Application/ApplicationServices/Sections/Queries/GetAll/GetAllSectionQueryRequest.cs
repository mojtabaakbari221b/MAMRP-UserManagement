namespace UserManagement.Application.ApplicationServices.Sections.Queries.GetAll;
public record GetAllSectionQueryRequest(int PageSize, int PageNumber) : IRequest<IEnumerable<SectionDto>>;