using UserManagement.Domain.Services;

namespace UserManagement.Domain.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IRoleManager Roles { get; }
    IUserManager Users { get; }
    ISectionRepository Sections { get; }
    ISectionGroupRepository SectionGroups { get; }
    Task SaveChangesAsync(CancellationToken token = default);
    Task BeginTransactionAsync(CancellationToken token = default);
    Task CommitTransactionAsync(CancellationToken token = default);
    Task RoleBackTransactionAsync(CancellationToken token = default);
}
