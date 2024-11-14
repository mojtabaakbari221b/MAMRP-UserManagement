namespace UserManagement.Application.ApplicationServices.UserRoles.Commands.ChangeRoleOfUser;

public sealed class ChangeRoleOfUserRequestHandler(IUnitOfWork uow) : IRequestHandler<ChangeRoleOfUserRequest>
{   
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(ChangeRoleOfUserRequest request, CancellationToken token)
    {
        await _uow.BeginTransactionAsync(token);
        var resultUserDto = await _uow.Users.GetUserById(request.UserId);
        if (resultUserDto.Data is null)
        {
            await _uow.RoleBackTransactionAsync(token);
            throw new UserNotFoundException();
        }

        await _uow.Users.RemoveUserRolesAndUserClaimsAsync(resultUserDto.Data.UserId);

        var resultRoleDto = await _uow.Roles.GetRoleById(request.RoleId);
        if (resultRoleDto.Data is null)
        {
            await _uow.RoleBackTransactionAsync(token);
            throw new RoleNotFoundException();
        }

        await _uow.Users.AddRoleAndTheirClaimsToUserAsync(resultUserDto.Data, resultRoleDto.Data);

        await _uow.CommitTransactionAsync(token);
    }
}