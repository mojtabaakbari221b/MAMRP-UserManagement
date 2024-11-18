namespace UserManagement.Application.ApplicationServices.Menus.Commands.Delete;

public sealed record DeleteMenuCommandRequest(long Id) : IRequest;