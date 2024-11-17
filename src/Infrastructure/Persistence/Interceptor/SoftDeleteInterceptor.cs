namespace UserManagement.Infrastructure.Persistence.Interceptor;

public sealed class SoftDeleteInterceptor(IHttpContextAccessor httpContextAccessor) : SaveChangesInterceptor
{
    private readonly HttpContext? _httpContext = httpContextAccessor.HttpContext;

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null) return result;

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Deleted, Entity: BaseEntity entity })
                continue;

            DeActiveEntity(entity, entry);
        }

        await ValueTask.CompletedTask;
        return result;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null)
            return result;

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Deleted, Entity: BaseEntity entity })
                continue;

            DeActiveEntity(entity, entry);
        }

        return result;
    }

    private void DeActiveEntity(BaseEntity entity, EntityEntry entry)
    {
        if (_httpContext.User.Identity is null || !_httpContext.User.Identity.IsAuthenticated)
        {
            return;
        }
        entity.IsActive = false;
        entry.State = EntityState.Modified;
    }
}