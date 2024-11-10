namespace UserManagement.Application.ApplicationServices.Sections.Commands.Add;

public record AddSectionCommandRequest(
    long GroupId,
    string Name,
    string Description,
    string Url,
    string Code,
    SectionType Type) : IRequest<SectionDto>;