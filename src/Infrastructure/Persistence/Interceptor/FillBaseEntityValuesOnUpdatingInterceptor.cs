namespace UserManagement.Infrastructure.Persistence.Interceptor;

public sealed class FillBaseEntityValuesOnUpdatingInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null) return result;

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Modified, Entity: BaseEntity entity }) continue;
            SetBaseEntityValues(entity);
        }

        await ValueTask.CompletedTask;
        return result;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null) return result;

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Modified, Entity: BaseEntity entity }) continue;
            SetBaseEntityValues(entity);
        }

        return result;
    }

    private void SetBaseEntityValues(BaseEntity entity)
    {
        // entity.UpdaterUser;
        
        entity.SetUpdateDatetime();
    }
}