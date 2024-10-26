using Microsoft.EntityFrameworkCore;

using UserManagement.Domain.Entities;


namespace UserManagement.Infrastructure.Persistence
{
    internal class UserManagementDbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuGroup> MenuGroups { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<ServiceGroup> ServiceGroups { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
