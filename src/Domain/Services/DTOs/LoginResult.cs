namespace UserManagement.Domain.Services.DTOs;

public record LoginResult(bool IsSuccess, Guid UserId);