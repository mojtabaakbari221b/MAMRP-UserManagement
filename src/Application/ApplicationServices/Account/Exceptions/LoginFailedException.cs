namespace UserManagement.Application.ApplicationServices.Account.Exceptions;

public class LoginFailedException()
    : MamrpBaseBadRequestException("نام کاربری یا رمزعبور شما اشتباه است.", ServicesCode.UserManagement);