namespace UserManagement.Domain.Services.DTOs;

public record UserDto(
    Guid UserId,
    string UserName,
    string FirstName,
    string FamilyName,
    string Email,
    string SecurityStamp);