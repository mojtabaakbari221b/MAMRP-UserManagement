﻿using Microsoft.AspNetCore.Identity;

namespace UserManagement.Infrastructure.Persistence.Identity;

public sealed class User : IdentityUser<Guid>
{
    public User()
    {
        GeneratedCode = Guid.NewGuid().ToString()[..8];
    }

    public string Name { get; set; }
    public string FamilyName { get; set; }
    public string GeneratedCode { get; set; }

    public ICollection<UserRefreshToken> UserRefreshTokens { get; set; } = [];

}