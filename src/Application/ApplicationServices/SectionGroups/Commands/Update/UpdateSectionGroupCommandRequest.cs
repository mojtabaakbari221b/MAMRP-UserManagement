namespace UserManagement.Application.ApplicationServices.SectionGroups.Commands.Update;

public sealed record UpdateSectionGroupCommandRequest(long Id, string Name) : IRequest;