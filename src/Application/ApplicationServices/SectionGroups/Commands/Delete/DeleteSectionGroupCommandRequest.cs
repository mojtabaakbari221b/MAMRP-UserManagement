namespace UserManagement.Application.ApplicationServices.SectionGroups.Commands.Delete;

public sealed record DeleteSectionGroupCommandRequest(long Id) : IRequest;