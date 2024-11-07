namespace UserManagement.Application.ApplicationServices.UserRoles.Commands.ChangeSectionClaimOfRole;

public sealed record ChangeSectionClaimOfRoleRequest(Guid RoleId, List<long> SelectionIds) : IRequest;