using Microsoft.AspNetCore.Identity;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Persistence.Identity;

public class UserClaim : IdentityUserClaim<Guid>
{
    public long SectionId { get; set; }
    public Section Section { get; set; } = default!;
    public bool IsActive { get; set; } = true;

}
