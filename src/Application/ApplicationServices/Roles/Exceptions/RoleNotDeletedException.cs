namespace UserManagement.Application.ApplicationServices.Roles.Exceptions;

public sealed class RoleNotDeletedException(List<string> errore)
    : MamrpBaseBadRequestException(errore, ServicesCode.UserManagement);