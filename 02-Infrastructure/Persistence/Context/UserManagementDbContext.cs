using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Persistence.Identity;


namespace UserManagement.Infrastructure.Persistence.Context
{
    public class UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options)
    {
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<SectionGroup> SectionGroups { get; set; }
        public DbSet<Section> Sections { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRefreshToken>().HasOne(u => u.User).WithMany().HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Section>().ToTable(nameof(Section));
            modelBuilder.Entity<SectionGroup>().ToTable(nameof(SectionGroup));

            modelBuilder.Entity<Section>().HasOne(u => u.Group).WithMany().HasForeignKey(u=>u.GroupId);

            modelBuilder.Entity<UserClaim>().Ignore(u => u.ClaimType);
            modelBuilder.Entity<UserClaim>().Ignore(u => u.ClaimValue);

            modelBuilder.Entity<RoleClaim>().Ignore(u => u.ClaimType);
            modelBuilder.Entity<RoleClaim>().Ignore(u => u.ClaimValue);
        }
    }
}
