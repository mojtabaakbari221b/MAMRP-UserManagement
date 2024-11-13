using System.Collections.Specialized;
using Share.Helper;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Repositories;

public interface ISectionRepository
{
    Task<Section> Add(Section section);
    void Delete(Section section);
    void Update(Section section);
    Task<Section?> FindAsync(long id, CancellationToken token = default);
    Task<IEnumerable<IResponse>> List(int pageSize, int pageNumber, CancellationToken token = default);
    Task<IResponse?> GetById(long id, CancellationToken token = default);
    Task<int> Count();
}