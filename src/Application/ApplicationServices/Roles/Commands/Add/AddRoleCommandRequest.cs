namespace UserManagement.Application.ApplicationServices.Roles.Commands.Add;

public sealed record AddRoleCommandRequest(string RoleName, string DisplayName) : IRequest<AddRoleCommandResponse>;

public sealed record AddRoleCommandResponse(string RoleName, string DisplayName);

