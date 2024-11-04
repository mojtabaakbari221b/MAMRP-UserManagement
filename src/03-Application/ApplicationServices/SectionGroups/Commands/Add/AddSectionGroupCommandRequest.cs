namespace UserManagement.Application.ApplicationServices.SectionGroups.Commands.Add;

public sealed record SectionGroupDto(long Id, string Name, SectionType Type);

public sealed record AddSectionGroupCommandRequest(string Name, SectionType Type)
    : IRequest<SectionGroupDto>;