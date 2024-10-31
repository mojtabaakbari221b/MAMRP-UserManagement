using Microsoft.AspNetCore.Identity;

namespace UserManagement.Infrastructure.Identity;

public class UserToken : IdentityUserToken<Guid>
{
    public UserToken()
    {
        GeneratedTime = DateTime.Now;
    }

    public User User { get; set; }
    public DateTime GeneratedTime { get; set; }

}