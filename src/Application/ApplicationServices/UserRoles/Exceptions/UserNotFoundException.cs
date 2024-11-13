namespace UserManagement.Application.ApplicationServices.UserRoles.Exceptions;

public class UserNotFoundException()
    : MamrpBaseNotFoundException("کاربر پیدا نشد.", ServicesCode.UserManagement);