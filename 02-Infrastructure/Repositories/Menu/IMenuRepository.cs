using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Repositories;


public interface ISectionRepository {
    Task<Section> Add(Section menu);
    IList<Section> List();
    Section GetById(long Id);
    void Delete(Section menu);
    void Update(Section menu);
}