namespace UserManagement.Application.ApplicationServices.MenuGroup.Commands.Update;

public sealed record UpdateMenuGroupCommandRequest(long Id, string Name) : IRequest;