using Microsoft.AspNetCore.Identity;

namespace UserManagement.Infrastructure.Persistence.Identity;

public class UserLogin : IdentityUserLogin<Guid>
{
    public UserLogin()
    {
        LoggedOn = DateTime.Now;
    }

    public DateTime LoggedOn { get; set; }
}