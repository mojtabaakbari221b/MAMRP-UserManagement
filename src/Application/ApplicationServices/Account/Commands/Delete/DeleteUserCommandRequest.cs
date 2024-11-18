namespace UserManagement.Application.ApplicationServices.Account.Commands.Delete;

public sealed record DeleteUserCommandRequest(Guid UserId) : IRequest;