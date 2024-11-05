namespace UserManagement.Application.ApplicationServices.Sections.Queries.GetAll;
public record GetAllSectionQueryRequest : IRequest<IEnumerable<SectionDto>>;