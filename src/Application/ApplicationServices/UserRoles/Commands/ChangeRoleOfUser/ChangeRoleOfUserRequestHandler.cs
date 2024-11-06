namespace UserManagement.Application.ApplicationServices.UserRoles.Commands.ChangeRoleOfUser;

public sealed class ChangeRoleOfUserRequestHandler(IAccountManager acountManager) : IRequestHandler<ChangeRoleOfUserRequest>
{   
    private readonly IAccountManager _acountManager = acountManager;
    public async Task Handle(ChangeRoleOfUserRequest request, CancellationToken cancellationToken)
    {
        var userDto = await _acountManager.GetUserById(request.UserId);
        await _acountManager.RemoveUserRolesAndUserClaimsAsync(userDto.Id);

        var roleDto = await _acountManager.GetRoleById(request.RoleId);
        await _acountManager.AddRoleAndTheirClaimsToUserAsync(userDto, roleDto);
    }
}