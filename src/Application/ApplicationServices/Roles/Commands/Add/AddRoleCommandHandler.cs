using UserManagement.Application.ApplicationServices.Roles.Exceptions;

namespace UserManagement.Application.ApplicationServices.Roles.Commands.Add;

public sealed class AddRoleCommandHandler(IAccountManager accountManager)
    : IRequestHandler<AddRoleCommandRequest, AddRoleCommandResponse>
{
    private readonly IAccountManager _accountManager = accountManager;

    public async Task<AddRoleCommandResponse> Handle(AddRoleCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _accountManager.AddRole(request.RoleName, request.DisplayName);
        if (!result)
        {
            throw new RoleAlredyExistException();
        }
        return request.Adapt<AddRoleCommandResponse>();
    }
}