namespace UserManagement.Domain.Services.DTOs;

public sealed record RegisterDto(string Username, string FirstName, string FamilyName, string Password);

public sealed record RegisterResult(bool Successed);