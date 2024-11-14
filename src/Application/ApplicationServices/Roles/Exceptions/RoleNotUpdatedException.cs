namespace UserManagement.Application.ApplicationServices.Roles.Exceptions;

public sealed class RoleNotUpdatedException(List<string> errors) 
    : MamrpBaseBadRequestException(errors, ServicesCode.UserManagement);