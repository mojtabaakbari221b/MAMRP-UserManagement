using Microsoft.AspNetCore.Http;
using Share.Extensions;

namespace UserManagement.Infrastructure.Persistence.Interceptor;

public sealed class FillBaseEntityValuesOnCreatingInterceptor(IdentityExtension identityExtension) : SaveChangesInterceptor
{
    private readonly IdentityExtension _identityExtension = identityExtension;
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null) 
            return result;

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Added, Entity: BaseEntity entity }) 
                continue;
            
            SetBaseEntityValues(entity);
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
            if (entry is not { State: EntityState.Added, Entity: BaseEntity entity }) 
                continue;
            
            SetBaseEntityValues(entity);
        }

        return result;
    }

    private void SetBaseEntityValues(BaseEntity entity)
    {
        var userId = _identityExtension.UserId();
        entity.RegisteringUser = userId;
        entity.UpdaterUser = userId;
    }
}