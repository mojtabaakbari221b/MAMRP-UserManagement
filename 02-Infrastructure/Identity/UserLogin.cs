using Microsoft.AspNetCore.Identity;

namespace UserManagement.Infrastructure.Identity;

public class UserLogin : IdentityUserLogin<Guid>
{
    public UserLogin()
    {
        LoggedOn = DateTime.Now;
    }

    public User User { get; set; }
    public DateTime LoggedOn { get; set; }
}