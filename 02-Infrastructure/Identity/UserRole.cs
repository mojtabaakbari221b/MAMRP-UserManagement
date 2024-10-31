using Microsoft.AspNetCore.Identity;

namespace UserManagement.Infrastructure.Identity;

public class UserRole : IdentityUserRole<int>
{
    public User User { get; set; }
    public Role Role { get; set; }
    public DateTime CreatedUserRoleDate { get; set; }

}