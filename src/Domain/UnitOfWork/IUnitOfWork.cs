namespace UserManagement.Domain.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    ISectionRepository Sections { get; }
    ISectionGroupRepository SectionGroups { get; }
    Task SaveChangeAsync(CancellationToken token = default);
}
