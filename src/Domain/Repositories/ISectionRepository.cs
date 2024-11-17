using System.Collections.Specialized;
using Share.Dtos;
using Share.Helper;
using Share.QueryFilterings;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Repositories;

public interface ISectionRepository
{
    Task AddAsync(Section section);
    void Delete(Section section);
    void Update(Section section);
    Task<Section?> FindAsync(long id, CancellationToken token = default);
    Task<ListDto> GetAll(PaginationFilter pagination, object filtering, object ordering, SectionType type, CancellationToken token = default);
    Task<IResponse?> GetById(long id, SectionType type, CancellationToken token = default);
    Task<bool> AnyAsync(string code, SectionType type, CancellationToken token = default);
}