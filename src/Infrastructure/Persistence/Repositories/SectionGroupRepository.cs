using Share.Dtos;
using Share.Ordering;
using UserManagement.Domain.Dtos;

namespace UserManagement.Infrastructure.Persistence.Repositories;

public class SectionGroupRepository(UserManagementDbContext context) : ISectionGroupRepository
{
    private readonly UserManagementDbContext _context = context;

    public async Task AddAsync(SectionGroup sectionGroup, CancellationToken token = default)
        => await _context.SectionGroups.AddAsync(sectionGroup, token);

    public async Task<bool> AnyAsync(long groupId, SectionType type, CancellationToken token = default)
        => await _context.SectionGroups.AsQueryable()
            .AnyAsync(g => g.Id == groupId && g.Type == type, token);

    public async Task<bool> AnyAsync(string name, SectionType type, CancellationToken token = default)
        => await _context.SectionGroups.AsQueryable()
            .AnyAsync(g => g.Name == name && g.Type == type, token);


    public void Update(SectionGroup sectionGroup)
        => _context.SectionGroups.Update(sectionGroup);

    public void Delete(SectionGroup sectionGroup)
        => _context.SectionGroups.Remove(sectionGroup);

    public async Task<ListDto> GetAll(PaginationFilter pagination, object? filtering, object? ordering,
        SectionType type,
        CancellationToken token)
    {
        var query = _context.SectionGroups.AsQueryable()
            .Where(x => x.Type == type);

        query = QueryFilter.Filter(query, filtering);

        query = QueryOrdering.ApplyOrdering(query, ordering);

        var count = await query.CountAsync(token);

        return new ListDto(
            count,
            await query.Select(c => c.Adapt<SectionGroupDto>())
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(token)
        );
    }

    public async Task<IResponse?> GetById(long id, SectionType type, CancellationToken token = default)
        => await _context.SectionGroups.AsQueryable()
            .Where(x => x.Id == id && x.Type == type)
            .Select(s => s.Adapt<SectionGroupDto>())
            .FirstOrDefaultAsync(token);

    public async Task<SectionGroup?> FindAsync(long id, SectionType type, CancellationToken token = default)
        => await _context.SectionGroups.AsQueryable()
            .Where(x => x.Id == id && x.Type == type)
            .FirstOrDefaultAsync(token);
}