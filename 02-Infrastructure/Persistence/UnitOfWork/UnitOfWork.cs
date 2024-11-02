using UserManagement.Domain.Repositories;
using UserManagement.Domain.UnitOfWork;
using UserManagement.Infrastructure.Persistence.Context;

namespace UserManagement.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork(UserManagementDbContext context,
                        ISectionRepository sectionRepository)
    : IUnitOfWork
{
    private readonly UserManagementDbContext _context = context;
    private readonly ISectionRepository _sectionRepository = sectionRepository;
    public ISectionRepository SectionRepository => _sectionRepository;

    public async Task Commit(CancellationToken token = default)
        => await _context.SaveChangesAsync(token);

    public void Dispose()
        => _context.Dispose();
    
}
