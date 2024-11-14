namespace UserManagement.Application.ApplicationServices.Roles.Exceptions;

public sealed class RoleNotAddedException(List<string> errors)
    : MamrpBaseBadRequestException(errors, ServicesCode.UserManagement);