namespace UserManagement.Domain.Services.DTOs;

public record UserDto(Guid Id, string UserName, string Email, string SecurityStamp);