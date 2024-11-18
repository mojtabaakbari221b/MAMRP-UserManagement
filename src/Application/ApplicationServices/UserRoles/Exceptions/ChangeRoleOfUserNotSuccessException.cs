namespace UserManagement.Application.ApplicationServices.UserRoles.Exceptions;

public sealed class ChangeRoleOfUserNotSuccessException(List<string> errors)
    : MamrpBaseBadRequestException(errors, ServicesCode.UserManagement);