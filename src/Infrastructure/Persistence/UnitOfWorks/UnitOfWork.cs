namespace UserManagement.Infrastructure.Persistence.UnitOfWorks;

public class UnitOfWork(
    UserManagementDbContext context,
    ISectionRepository sections,
    ISectionGroupRepository sectionGroups,
    IRoleManager roles,
    IUserManager users)
    : IUnitOfWork
{
    private IDbContextTransaction _transaction;
    private readonly UserManagementDbContext _context = context;

    public IRoleManager Roles { get; } = roles;
    public IUserManager Users { get; } = users;
    public ISectionRepository Sections { get; } = sections;
    public ISectionGroupRepository SectionGroups { get; } = sectionGroups;

    public async Task SaveChangesAsync(CancellationToken token = default)
    {
        await _context.SaveChangesAsync(token);
    }

    public async Task BeginTransactionAsync(CancellationToken token = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(token);
    }

    public async Task CommitTransactionAsync(CancellationToken token = default)
    {
        try
        {
            await _transaction.CommitAsync(token);
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }

    public async Task RoleBackTransactionAsync(CancellationToken token = default)
    {
        try
        {
            await _transaction.RollbackAsync(token);
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }

    private async Task DisposeTransactionAsync()
    {
        await _transaction.DisposeAsync();
    }

    public void Dispose()
    {
        DisposeTransactionAsync().GetAwaiter().GetResult();
        _context.Dispose();
    }
}