using UserManagement.Application.ExternalServices.Identities.DTOs;

namespace UserManagement.Application.ExternalServices.Identities;

public interface ITokenFactory
{
    Task<TokenDto> CreateTokenAsync(Guid userId);
}