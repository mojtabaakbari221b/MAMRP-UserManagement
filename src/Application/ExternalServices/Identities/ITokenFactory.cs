using UserManagement.Application.ExternalServices.Identities.DTOs;
using UserManagement.Domain.Services.DTOs;

namespace UserManagement.Application.ExternalServices.Identities;

public interface ITokenFactory
{
    Task<TokenResult> CreateTokenAsync(Guid userId);
}