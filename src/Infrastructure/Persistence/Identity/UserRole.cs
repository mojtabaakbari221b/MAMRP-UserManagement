using Microsoft.AspNetCore.Identity;

namespace UserManagement.Infrastructure.Persistence.Identity;

public class UserRole : IdentityUserRole<Guid>
{
    public DateTime CreatedUserRoleDate { get; set; }
    public bool IsActive { get; set; } = true;

}

