namespace UserManagement.Application.ApplicationServices.UserRoles.Commands.ChangeRoleOfUser;

public sealed class ChangeRoleOfUserRequestHandler(IUnitOfWork uow) : IRequestHandler<ChangeRoleOfUserRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(ChangeRoleOfUserRequest request, CancellationToken token)
    {
        await _uow.BeginTransactionAsync(token);
        var userDto = await _uow.Users.GetUserById(request.UserId);
        if (userDto is null)
        {
            await _uow.RoleBackTransactionAsync(token);
            throw new UserNotFoundException();
        }

        await _uow.Users.RemoveUserRolesAndUserClaimsAsync(userDto.Id);

        var roleDto = await _uow.Roles.GetRoleById(request.RoleId);
        if (roleDto is null)
        {
            await _uow.RoleBackTransactionAsync(token);
            throw new RoleNotFoundException();
        }

        await _uow.Users.AddRoleAndTheirClaimsToUserAsync(userDto, roleDto);

        await _uow.CommitTransactionAsync(token);
    }
}