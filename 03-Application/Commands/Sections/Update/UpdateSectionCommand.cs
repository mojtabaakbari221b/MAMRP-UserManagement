using MediatR;

namespace UserManagement.Application.Commands.Sections.Update;

public record UpdateSectionCommand(long Id, long GroupId, string Name, string Url, string Description) : IRequest;
