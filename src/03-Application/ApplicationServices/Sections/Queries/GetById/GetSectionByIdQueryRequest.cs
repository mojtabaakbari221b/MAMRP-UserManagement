namespace UserManagement.Application.ApplicationServices.Sections.Queries.GetById;


public record GetSectionByIdQueryRequest(long Id) : IRequest<SectionDto>;
