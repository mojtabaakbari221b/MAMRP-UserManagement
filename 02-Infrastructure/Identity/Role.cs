using Microsoft.AspNetCore.Identity;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Identity;

public class Role : IdentityRole<Guid>
{
    public Role()
    {
        CreatedDate = DateTime.Now;
    }

    public string DisplayName { get; set; }
    public DateTime CreatedDate { get; set; }
    public ICollection<Section> Sections { get; set; }
    public ICollection<UserRole> Users { get; set; }


}