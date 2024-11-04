using Microsoft.AspNetCore.Identity;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Persistence.Identity;

public class RoleClaim : IdentityRoleClaim<Guid>
{
    public long SectionId { get; set; }
    public Section Section { get; set; } = default!;
}
