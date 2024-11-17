using UserManagement.Application.ApplicationServices.ServiceGroups.Queries.GetAll;
using UserManagement.Application.ApplicationServices.ServiceGroups.Queries.GetById;
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

    public void Update(SectionGroup sectionGroup)
        => _context.SectionGroups.Update(sectionGroup);

    public void Delete(SectionGroup sectionGroup)
        => _context.SectionGroups.Remove(sectionGroup);

    public async Task<IEnumerable<IResponse>> ToList(int page, int size, SectionType type,
        CancellationToken token)
        => await _context.SectionGroups.AsQueryable()
            .Select(s => s.Adapt<SectionGroupDto>())
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(token);

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