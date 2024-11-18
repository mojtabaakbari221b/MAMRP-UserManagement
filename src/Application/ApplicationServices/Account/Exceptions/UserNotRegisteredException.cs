namespace UserManagement.Application.ApplicationServices.Account.Exceptions;

public sealed class UserNotRegisteredException(List<string> errors)
    : MamrpBaseBadRequestException(errors, ServicesCode.UserManagement)
{
}