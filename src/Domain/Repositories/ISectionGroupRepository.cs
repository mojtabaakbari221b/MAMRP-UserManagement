using Share.Dtos;
using Share.QueryFilterings;
using UserManagement.Domain.Filterings;
using UserManagement.Domain.Orderings;

namespace UserManagement.Domain.Repositories;

public interface ISectionGroupRepository
{
    void Update(SectionGroup sectionGroup);
    void Delete(SectionGroup sectionGroup);
    Task AddAsync(SectionGroup sectionGroup, CancellationToken token = default);
    Task<SectionGroup?> FindAsync(long id, SectionType type, CancellationToken token = default);
    Task<bool> AnyAsync(long groupId, SectionType type, CancellationToken token = default);
    Task<bool> AnyAsync(string name, SectionType type, CancellationToken token = default);

    Task<ListDto> GetAll(PaginationFilter pagination, object? filtering,
        object? ordering, SectionType type, CancellationToken token = default);

    Task<IResponse?> GetById(long id, SectionType type, CancellationToken token = default);
}