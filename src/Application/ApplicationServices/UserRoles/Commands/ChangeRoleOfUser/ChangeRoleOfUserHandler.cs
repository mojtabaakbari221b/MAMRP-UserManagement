namespace UserManagement.Application.ApplicationServices.UserRoles.Commands.ChangeRoleOfUser;

public sealed class ChangeRoleOfUserHandler(IUnitOfWork uow)
    : IRequestHandler<ChangeRoleOfUserRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(ChangeRoleOfUserRequest request, CancellationToken token)
    {
        await _uow.BeginTransactionAsync(token);
        try
        {
            var resultUserDto = await _uow.Users.GetUserById(request.UserId);
            if (resultUserDto.Data is null)
            {
                throw new UserNotFoundException();
            }

            await _uow.Users.RemoveUserRolesAndUserClaimsAsync(resultUserDto.Data.UserId);
            await _uow.SaveChangesAsync(token); // ثبت تغییرات حذف

            var resultRoleDto = await _uow.Roles.GetRoleById(request.RoleId);
            if (resultRoleDto.Data is null)
            {
                throw new RoleNotFoundException();
            }

            var result = await _uow.Users.AddRoleAndTheirClaimsToUserAsync(resultUserDto.Data, resultRoleDto.Data);
            if (!result.IsSuccess)
            {
                throw new ChangeRoleOfUserNotSuccessException(result.Errors);
            }

            await _uow.SaveChangesAsync(token); // ثبت تغییرات اضافه
            await _uow.CommitTransactionAsync(token); // نهایی کردن تراکنش
        }
        catch
        {
            await _uow.RoleBackTransactionAsync(token); // بازگرداندن تراکنش در صورت خطا
            throw;
        }
    }
}