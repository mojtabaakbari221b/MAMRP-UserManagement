namespace UserManagement.Infrastructure.Persistence.Identity;


public class Role : IdentityRole<Guid>
{
    public required string DisplayName { get; set; }
    public DateTime CreatedDate { get; init; } = DateTime.Now;
    public bool IsActive { get; set; } = true;
}
