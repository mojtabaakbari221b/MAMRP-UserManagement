using Share.Helper;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Persistence.Identity;

public class UserRefreshToken : BaseEntity
{
    public User? User { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public bool IsValid { get; set; }
}