namespace UserManagement.Infrastructure.Persistence.Interceptor;

public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null) return result;

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Deleted, Entity: BaseEntity entity }) continue;

            entity.IsActive = false;
            entity.UpdateDatetime = DateTime.Now;
            entity.PersianUpdateDatetime = new DateTime().ToPersianDateTime();
            entry.State = EntityState.Modified;
        }

        await ValueTask.CompletedTask;
        return result;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null) return result;

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Deleted, Entity: BaseEntity entity }) continue;

            entity.IsActive = false;
            entity.UpdateDatetime = DateTime.Now;
            entity.PersianUpdateDatetime = new DateTime().ToPersianDateTime();
            entry.State = EntityState.Modified;
        }

        return result;
    }
}