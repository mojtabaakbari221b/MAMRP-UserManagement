namespace UserManagement.Infrastructure.Persistence.Repositories;

public class SectionGroupRepository(UserManagementDbContext context) : ISectionGroupRepository
{
    private readonly UserManagementDbContext _context = context;

    public async Task<SectionGroup> AddAsync(SectionGroup sectionGroup, CancellationToken token = default)
    {
        var entityEntry = await _context.SectionGroups.AddAsync(sectionGroup, token);
        return entityEntry.Entity;
    }

    public async Task<bool> AnyAsync(long groupId, CancellationToken token = default)
        => await _context.SectionGroups.AsQueryable()
            .AnyAsync(group => group.Id == groupId, token);
}