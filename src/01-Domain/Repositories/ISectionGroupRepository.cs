namespace UserManagement.Domain.Repositories;

public interface ISectionGroupRepository
{
    Task<SectionGroup> AddAsync(SectionGroup sectionGroup, CancellationToken token = default);
    Task<bool> AnyAsync(long groupId, CancellationToken token = default);
}