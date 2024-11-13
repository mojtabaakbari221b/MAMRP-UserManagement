namespace UserManagement.Infrastructure.Persistence.Repositories;


public class SectionGroupRepository(UserManagementDbContext context) : ISectionGroupRepository
{
    private readonly UserManagementDbContext _context = context;

    public async Task AddAsync(SectionGroup sectionGroup, CancellationToken token = default)
    {
        await _context.SectionGroups.AddAsync(sectionGroup, token);
    }

    public async Task<bool> AnyAsync(long groupId, CancellationToken token = default)
        => await _context.SectionGroups.AsQueryable()
            .AnyAsync(group => group.Id == groupId, token);

    public void Update(SectionGroup sectionGroup)
        => _context.SectionGroups.Update(sectionGroup);

    public void Delete(SectionGroup sectionGroup)
        => _context.SectionGroups.Remove(sectionGroup);

    public async Task<IEnumerable<IResponse>> ToList(int page, int size, SectionType type,
        CancellationToken token)
        => await _context.SectionGroups.AsQueryable()
            .Select(s => s.Adapt<GetAllSectionGroupQueryResponse>())
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(token);

    public async Task<IResponse?> GetById(long id, CancellationToken token = default)
        => await _context.SectionGroups.AsQueryable()
            .Where(x => x.Id == id)
            .Select(s => new GetSectionGroupByIdQueryResponse
            (
                s.Id,
                s.Name,
                s.Type
            ))
            .FirstOrDefaultAsync(token);

    public async Task<SectionGroup?> FindAsync(long id, CancellationToken token = default)
        => await _context.SectionGroups.AsQueryable()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(token);
}