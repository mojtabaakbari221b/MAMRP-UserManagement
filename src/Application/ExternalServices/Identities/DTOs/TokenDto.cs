namespace UserManagement.Application.ExternalServices.Identities.DTOs;

public record TokenDto(string AccessToken, string RefreshToken);