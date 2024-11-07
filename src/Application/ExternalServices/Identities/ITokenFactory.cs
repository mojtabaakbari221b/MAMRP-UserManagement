using UserManagement.Domain.Services.DTOs;

namespace UserManagement.Application.ExternalServices.Identities;

public interface ITokenFactory
{
    Task<TokenDto> CreateTokenAsync(Guid userId);
}