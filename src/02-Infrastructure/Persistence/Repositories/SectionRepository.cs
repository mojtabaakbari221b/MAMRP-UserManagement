using Microsoft.EntityFrameworkCore;
using Share.Helper;
using UserManagement.Application.ApplicationServices.Sections.Dtos;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Persistence.Context;

namespace UserManagement.Infrastructure.Persistence.Repositories;

public sealed class SectionRepository(UserManagementDbContext context) : ISectionRepository
{
    private readonly UserManagementDbContext _context = context;

    public async Task<Section> Add(Section section)
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

    public async Task<IResponse?> GetById(long id, CancellationToken token = default)
        => await _context.Sections.AsQueryable()
            .Where(c => c.Id == id)
            .Select(c => new SectionDto(c.Id, c.Name, c.Url, c.GroupId, c.Description))
            .FirstOrDefaultAsync(token);
    
    public async Task<IEnumerable<IResponse>> List(CancellationToken token = default)
        => await _context.Sections.AsQueryable()
            .Select(s => new SectionDto(s.Id, s.Name, s.Url, s.GroupId, s.Description))
            .ToListAsync(token);

    
}