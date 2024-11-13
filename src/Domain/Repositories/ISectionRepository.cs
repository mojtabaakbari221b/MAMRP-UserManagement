using System.Collections.Specialized;
using Share.Dtos;
using Share.Helper;
using Share.QueryFilterings;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Repositories;

public interface ISectionRepository
{
    Task<Section> AddAsync(Section section);
    void Delete(Section section);
    void Update(Section section);
    Task<Section?> FindAsync(long id, CancellationToken token = default);
    Task<int> Count(SectionType type);
    Task<ListDto> GetAll(PaginationFilter pagination, object filtering, SectionType type, CancellationToken token = default);
    Task<IResponse?> GetById(long id, SectionType type, CancellationToken token = default);
}