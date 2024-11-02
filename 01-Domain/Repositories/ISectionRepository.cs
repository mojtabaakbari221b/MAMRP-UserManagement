using UserManagement.Domain.Common;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Repositories;


public interface ISectionRepository {
    Task Add(Section section);
    Task<IList<IResponse>> List();
    Task<Section?> FindAsync(long Id);
    Task<IResponse?> GetById(long Id);
    void Delete(Section section);
    void Update(Section mesectionnu);
}