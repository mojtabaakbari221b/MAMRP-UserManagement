namespace UserManagement.Application.ApplicationServices.UserRoles.Commands.ChangeSectionClaimOfUser;

public sealed record ChangeSectionClaimOfUserRequest(Guid UserId, List<long> SelectionIds) : IRequest;