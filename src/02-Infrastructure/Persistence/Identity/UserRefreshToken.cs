using Share.Helper;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Persistence.Identity;

public class UserRefreshToken : BaseEntity
{
    public UserRefreshToken()
    {
        CreatedAt = DateTime.Now;
    }

    public User User { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; init; }
    public bool IsValid { get; set; }
}