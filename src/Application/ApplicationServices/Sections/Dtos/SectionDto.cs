namespace UserManagement.Application.ApplicationServices.Sections.Dtos;

public sealed record SectionDto(
    long Id,
    long GroupId,
    string Name,
    string Url,
    string Code,
    string Description,
    SectionType Type) : IResponse;