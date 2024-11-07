namespace UserManagement.Application.ApplicationServices.UserRole.Commands.ChangeSectionClaimOfRole;

public sealed record ChangeSectionClaimOfRoleRequest(Guid RoleId, List<long> SelectionIds) : IRequest;