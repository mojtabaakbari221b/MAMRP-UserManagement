using System.Data;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Common;
using UserManagement.Application.Dtos.SectionDtos;

namespace UserManagement.Infrastructure.Repositories;


public class SectionRepository(UserManagementDbContext context) : ISectionRepository
{
    private readonly UserManagementDbContext _context = context;

    public async Task Add(Section section)
        => await _context.Sections.AddAsync(section);

    public void Delete(Section section)
        => _context.Sections.Remove(section);

    public async Task<Section?> FindAsync(long Id)
        => await _context.Sections.AsQueryable()
                                    .Where(u => u.Id == Id)
                                    .FirstOrDefaultAsync();

    public async Task<IList<IResponse>> List()
        => (IList<IResponse>)await _context.Sections.AsQueryable()
                                    .Select(s => new SectionDto(s.Id, s.Name, s.Url, s.GroupId, s.Description))
                                    .ToListAsync();

    public void Update(Section section)
        => _context.Sections.Update(section);
}