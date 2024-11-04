namespace UserManagement.Application.ApplicationServices.Sections.Commands.Add;

public record AddSectionCommandRequest(
    long GroupId,
    string Name,
    string Description,
    string Url,
    SectionType Type) : IRequest<SectionDto>;