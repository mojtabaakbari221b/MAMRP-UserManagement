namespace UserManagement.Application.ExternalServices.Identities.DTOs;

public record TokenResult(string AccessToken, string RefreshToken, Guid UserId);