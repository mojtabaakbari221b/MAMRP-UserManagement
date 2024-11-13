using UserManagement.Application.ApplicationServices.Menus.Dtos;

namespace UserManagement.Infrastructure.Persistence.Repositories;

public sealed class SectionRepository(UserManagementDbContext context) : ISectionRepository
{
    private readonly UserManagementDbContext _context = context;

    public async Task<Section> AddAsync(Section section)
    {
        var entityEntry = await _context.Sections.AddAsync(section);
        return entityEntry.Entity;
    }

    public void Update(Section section)
        => _context.Sections.Update(section);

    public void Delete(Section section)
        => _context.Sections.Remove(section);

    public async Task<Section?> FindAsync(long id, CancellationToken token = default)
        => await _context.Sections.AsQueryable()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync(token);

    public async Task<IEnumerable<IResponse>> GetAllServices(int pageNumber, int pageSize, CancellationToken token = default)
        => await _context.Sections.AsQueryable()
            .Where(x => x.Type == SectionType.Service)
            .Select(c => c.Adapt<ServiceDto>())
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);

    public async Task<IEnumerable<IResponse>> GetAllMenus(int pageNumber, int pageSize, CancellationToken token = default)
        => await _context.Sections.AsQueryable()
            .Where(x => x.Type == SectionType.Menu)
            .Select(c => c.Adapt<MenuDto>())
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);

    public async Task<IResponse?> GetByIdService(long id, CancellationToken token = default)
        => await _context.Sections.AsQueryable()
            .Where(c => c.Id == id)
            .Select(c => c.Adapt<ServiceDto>())
            .FirstOrDefaultAsync(token);
    public async Task<IResponse?> GetByIdMenu(long id, CancellationToken token = default)
        => await _context.Sections.AsQueryable()
            .Where(c => c.Id == id)
            .Select(c => c.Adapt<MenuDto>())
            .FirstOrDefaultAsync(token);
}