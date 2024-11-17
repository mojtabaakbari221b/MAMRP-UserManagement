using Share.Extensions;

namespace UserManagement.Infrastructure.Persistence.Interceptor;

public sealed class FillBaseEntityValuesOnUpdatingInterceptor(IdentityExtension identityExtension) : SaveChangesInterceptor
{
    private readonly IdentityExtension _identityExtension = identityExtension;
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
        var userId = _identityExtension.UserId();
        entity.UpdaterUser = userId;
        
        entity.SetUpdateDatetime();
    }
}