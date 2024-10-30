using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Repositories;


public interface IMenuRepository {
    void Add(Menu menu);
}