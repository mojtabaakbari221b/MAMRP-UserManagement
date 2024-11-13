namespace UserManagement.Domain.Repositories;

public interface ISectionRepository
{
    Task<Section> AddAsync(Section section);
    void Delete(Section section);
    void Update(Section section);
    Task<Section?> FindAsync(long id, CancellationToken token = default);
    Task<IEnumerable<IResponse>> GetAllServices(int pageNumber, int pageSize, CancellationToken token = default);
    Task<IEnumerable<IResponse>> GetAllMenus(int pageNumber, int pageSize, CancellationToken token = default);
    Task<IResponse?> GetByIdService(long id, CancellationToken token = default);
}