using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Repositories;


public interface IMenuRepository {
    void Add(Menu menu);
    IList<Menu> List();
    Menu GetById(long Id);
    void Delete(Menu menu);
    void Update(Menu menu);
}