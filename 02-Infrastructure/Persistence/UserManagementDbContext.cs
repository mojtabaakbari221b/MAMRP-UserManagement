using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Identity;


namespace UserManagement.Infrastructure.Persistence
{
    public class UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : DbContext(options)
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<SectionGroup> SectionGroups { get; set; }
        public DbSet<Section> Sections { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Section>().ToTable(nameof(Section));
            modelBuilder.Entity<SectionGroup>().ToTable(nameof(SectionGroup));

            modelBuilder.Entity<Section>().HasOne(nameof(SectionGroups)).WithMany().HasForeignKey("GroupId");
        }
    }
}
