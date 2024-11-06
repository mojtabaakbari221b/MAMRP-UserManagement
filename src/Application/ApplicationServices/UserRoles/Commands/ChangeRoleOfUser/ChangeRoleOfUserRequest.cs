namespace UserManagement.Application.ApplicationServices.UserRoles.Commands.ChangeRoleOfUser;

public sealed record ChangeRoleOfUserRequest(string UserId, string RoleId) : IRequest;