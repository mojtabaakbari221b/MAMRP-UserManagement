namespace UserManagement.Application.ApplicationServices.Sections.Dtos;

public sealed record SectionDto(
    long Id,
    string Name,
    string Url,
    long GroupId,
    string Description) : IResponse;