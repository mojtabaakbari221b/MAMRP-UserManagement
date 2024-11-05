using Microsoft.AspNetCore.Identity;

namespace UserManagement.Infrastructure.Persistence.Identity;

public class UserToken : IdentityUserToken<Guid>
{
    public UserToken()
    {
        GeneratedTime = DateTime.Now;
    }

    public DateTime GeneratedTime { get; set; }

}