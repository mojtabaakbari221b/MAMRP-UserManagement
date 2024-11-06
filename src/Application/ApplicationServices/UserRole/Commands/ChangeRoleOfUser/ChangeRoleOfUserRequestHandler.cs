namespace UserManagement.Application.ApplicationServices.UserRole.Commands.ChangeRoleOfUser;

public class ChangeRoleOfUserRequestHandler(IAccountManager acountManager) : IRequestHandler<ChangeRoleOfUserRequest>
{   
    private readonly IAccountManager _acountManager = acountManager;
    public async Task Handle(ChangeRoleOfUserRequest request, CancellationToken cancellationToken)
    {
        var userDto = await _acountManager.GetUserById(request.userId);
        await _acountManager.RemoveUserRolesAndUserClaimsAsync(userDto.Id);

        var roleDto = await _acountManager.GetRoleById(request.roleId);
        await _acountManager.AddRoleAndTheirClaimsToUserAsync(userDto, roleDto);
    }
}