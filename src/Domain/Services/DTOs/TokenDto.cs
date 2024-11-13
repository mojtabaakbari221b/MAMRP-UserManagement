namespace UserManagement.Domain.Services.DTOs;

public sealed record TokenDto(string AccessToken, string RefreshToken, Guid UserId);