namespace UserManagement.Domain.Services.DTOs;

public sealed record RegisterDto(
    string UserName,
    string FirstName,
    string FamilyName,
    string Password);

public sealed record RegisterResult(bool Successed);