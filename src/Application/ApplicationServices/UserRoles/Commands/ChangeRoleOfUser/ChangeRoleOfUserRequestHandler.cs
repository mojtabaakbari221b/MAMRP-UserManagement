namespace UserManagement.Application.ApplicationServices.UserRoles.Commands.ChangeRoleOfUser;

public sealed class ChangeRoleOfUserRequestHandler(IUnitOfWork uow) : IRequestHandler<ChangeRoleOfUserRequest>
{   
    private readonly IUnitOfWork _uow = uow;
    public async Task Handle(ChangeRoleOfUserRequest request, CancellationToken token)
    {
        await _uow.BeginTransactionAsync(token);
        try
        {
            var userDto = await _uow.Users.GetUserById(request.UserId);
            await _uow.Users.RemoveUserRolesAndUserClaimsAsync(userDto.Id);
            
            var roleDto = await _uow.Roles.GetRoleById(request.RoleId);
            await _uow.Users.AddRoleAndTheirClaimsToUserAsync(userDto, roleDto);
            
            await _uow.CommitTransactionAsync(token);
        }
        catch
        {
            await _uow.RoleBackTransactionAsync(token);
            throw;
        }
    }
}