namespace UserManagement.Application.ApplicationServices.MenuGroup.Commands.Delete;

public sealed record DeleteMenuGroupCommandRequest(long Id) : IRequest;