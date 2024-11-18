namespace UserManagement.Infrastructure.Persistence.UnitOfWorks;


public class UnitOfWork(
    UserManagementDbContext context,
    ISectionRepository sections,
    ISectionGroupRepository sectionGroups,
    IRoleManager roles,
    IUserManager users)
    : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
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
        if (_transaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }

        _transaction = await _context.Database.BeginTransactionAsync(token);
    }

    public async Task CommitTransactionAsync(CancellationToken token = default)
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction is in progress to commit.");
        }

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
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction is in progress to rollback.");
        }

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
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        DisposeTransactionAsync().GetAwaiter().GetResult();
        _context.Dispose();
    }
}
