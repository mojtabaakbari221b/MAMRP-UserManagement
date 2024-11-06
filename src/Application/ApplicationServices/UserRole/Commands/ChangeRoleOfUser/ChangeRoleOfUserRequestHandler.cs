using MediatR;

namespace UserManagement.Application.ApplicationServices.UserRole.Commands.ChangeRoleOfUser;

public class ChangeRoleOfUserRequestHandler(IAcountManager acountManager) : IRequestHandler<ChangeRoleOfUserRequest>
{   
    private readonly IAcountManager _acountManager = acountManager;
    public async Task Handle(ChangeRoleOfUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _acountManager.GetById(request.userId);
        await _acountManager.RemoveUserRolesAndUserClaimsAsync(user.Id);
        
    }
}