using UserManagement.Domain.Repositories;

namespace UserManagement.Domain.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    ISectionRepository SectionRepository { get; }
    Task Commit(CancellationToken token = default);
}
