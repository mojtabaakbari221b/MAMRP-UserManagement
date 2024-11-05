namespace UserManagement.Application.ExternalServices.Identities.DTOs;

public record LoginResult(bool IsSuccess, Guid UserId);