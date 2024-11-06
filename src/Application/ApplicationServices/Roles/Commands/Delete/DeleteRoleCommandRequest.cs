namespace UserManagement.Application.ApplicationServices.Roles.Commands.Delete;

public sealed record DeleteRoleCommandRequest(Guid Id) : IRequest;