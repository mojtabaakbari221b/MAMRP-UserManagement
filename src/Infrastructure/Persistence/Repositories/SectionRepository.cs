using UserManagement.Application.ApplicationServices.Sections.Dtos;


namespace UserManagement.Infrastructure.Persistence.Repositories;

public sealed class SectionRepository(UserManagementDbContext context): ISectionRepository {
    private readonly UserManagementDbContext _context = context;

    public async Task < Section > Add(Section section) {
        var entityEntry = await _context.Sections.AddAsync(section);
        return entityEntry.Entity;
    }

    public void Update(Section section) => _context.Sections.Update(section);

    public void Delete(Section section) => _context.Sections.Remove(section);

    public async Task < Section ? > FindAsync(long id, CancellationToken token =
        default) => await _context.Sections.AsQueryable()
        .Where(u => u.Id == id)
        .FirstOrDefaultAsync(token);

    public async Task < IResponse ? > GetById(long id, CancellationToken token =
        default) => await _context.Sections.AsQueryable()
        .Where(c => c.Id == id)
        .Select(c => new SectionDto(c.Id, c.GroupId, c.Name, c.Url, c.Code, c.Description, c.Type))
        .FirstOrDefaultAsync(token);

    public async Task < IEnumerable < IResponse >> List(int pageSize, int pageNumber, CancellationToken token =
        default) => await _context.Sections.Skip((pageNumber - 1) * pageSize).Take(pageSize).AsQueryable()
        .Select(c => new SectionDto(c.Id, c.GroupId, c.Name, c.Url, c.Code, c.Description, c.Type))
        .ToListAsync(token);

}