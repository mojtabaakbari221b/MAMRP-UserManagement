namespace UserManagement.Infrastructure.Persistence.Identity;

public class UserToken : IdentityUserToken<Guid>
{
    public UserToken()
    {
        GeneratedTime = DateTime.Now;
    }

    public DateTime GeneratedTime { get; set; }
    public bool IsActive { get; set; } = true;

}