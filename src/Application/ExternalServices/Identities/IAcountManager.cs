using UserManagement.Application.ExternalServices.Identities.DTOs;

namespace UserManagement.Application.ExternalServices.Identities;

public interface IAcountManager
{
    Task<LoginResult> Login(string username, string password);
    Task Register(RegisterDto registerDto);
}