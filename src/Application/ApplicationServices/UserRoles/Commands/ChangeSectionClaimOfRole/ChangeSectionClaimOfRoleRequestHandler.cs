namespace UserManagement.Application.ApplicationServices.UserRoles.Commands.ChangeSectionClaimOfRole;

public class ChangeSectionClaimOfRoleRequestHandler(IAccountManager acountManager) : IRequestHandler<ChangeSectionClaimOfRoleRequest>
{
    private readonly IAccountManager _acountManager = acountManager;
    public async Task Handle(ChangeSectionClaimOfRoleRequest request, CancellationToken cancellationToken)
    {
        var roleDto = new RoleDto(request.RoleId, null);
        await _acountManager.RemoveSectionClaimOfRoleAsync(roleDto);
        await _acountManager.AddSectionIdsToRoleClaimAsync(roleDto, request.SelectionIds);
    }
}