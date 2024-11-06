namespace UserManagement.Application.ApplicationServices.UserRole.Commands.ChangeRoleOfUser;

public record ChangeRoleOfUserRequest(string userId, Guid roleId) : IRequest;