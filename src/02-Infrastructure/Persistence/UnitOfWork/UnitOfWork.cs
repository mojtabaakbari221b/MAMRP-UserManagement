using UserManagement.Domain.Repositories;
using UserManagement.Domain.UnitOfWork;
using UserManagement.Infrastructure.Persistence.Context;

namespace UserManagement.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork(UserManagementDbContext context,
                        ISectionRepository sections,
                        ISectionGroupRepository sectionGroups)
    : IUnitOfWork
{
    private readonly UserManagementDbContext _context = context;
    public ISectionRepository Sections { get; } = sections;

    public ISectionGroupRepository SectionGroups { get; } = sectionGroups;

    public async Task SaveChangeAsync(CancellationToken token = default)
        => await _context.SaveChangesAsync(token);

    public void Dispose()
        => _context.Dispose();
    
}
