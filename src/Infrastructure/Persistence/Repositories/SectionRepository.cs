using Share.Dtos;
using Share.Ordering;
using Share.QueryFilterings;
using UserManagement.Domain.Filterings;

namespace UserManagement.Infrastructure.Persistence.Repositories;

public sealed class SectionRepository(UserManagementDbContext context) : ISectionRepository
{
    private readonly UserManagementDbContext _context = context;

    public async Task AddAsync(Section section)
        => await _context.Sections.AddAsync(section);

    public void Update(Section section)
        => _context.Sections.Update(section);


    public void Delete(Section section)
        => _context.Sections.Remove(section);

    public async Task<Section?> FindAsync(long id, CancellationToken token =
        default) => await _context.Sections.AsQueryable()
        .Where(u => u.Id == id)
        .FirstOrDefaultAsync(token);

    public async Task<ListDto> GetAll(
        PaginationFilter pagination,
        object? filtering,
        object? ordering,
        SectionType type,
        CancellationToken token = default
    )
    {
        var query = _context.Sections.AsQueryable()
            .Where(x => x.Type == type);

        query = QueryFilter.Filter(query, filtering);

        query = QueryOrdering.ApplyOrdering(query, ordering);

        var count = await query.CountAsync(token);

        return new ListDto(
            count,
            await query.Select(c => c.Adapt<MenuDto>())
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(token)
        );
    }

    public async Task<IResponse?> GetById(long id, SectionType type, CancellationToken token = default)
        => await _context.Sections.AsQueryable()
            .Where(c => c.Type == type)
            .Where(c => c.Id == id)
            .Select(c => c.Adapt<ServiceDto>())
            .FirstOrDefaultAsync(token);

    public async Task<bool> AnyAsync(string code, SectionType type, CancellationToken token = default)
    {
        var any = _context.Sections.Any(s => s.Code == code && s.Type == type);
        return any;
    }
}