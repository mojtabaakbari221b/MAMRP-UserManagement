namespace UserManagement.Domain.Repositories;

public interface ISectionGroupRepository
{
    void Update(SectionGroup sectionGroup);
    void Delete(SectionGroup sectionGroup);
    Task AddAsync(SectionGroup sectionGroup, CancellationToken token = default);
    Task<SectionGroup?> FindAsync(long id, CancellationToken token = default);
    Task<bool> AnyAsync(long groupId, CancellationToken token = default);
    Task<IEnumerable<IResponse>> ToList(int page, int size, SectionType type, CancellationToken token = default);
    Task<IResponse?> GetById(long id, CancellationToken token = default);
}