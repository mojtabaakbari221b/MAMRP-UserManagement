namespace UserManagement.Application.ApplicationServices.Account.Exceptions;

public class LoginFailedException()
    : MamrpBaseBadRequestException("Login failed: Invalid credentials.");