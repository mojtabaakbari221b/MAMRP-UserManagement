namespace UserManagement.Infrastructure.Persistence.SeedData;

public interface IServiceSeedData
{
    void SeedData();
}

public sealed class ServiceSeedData(IServiceScopeFactory serviceScope) : IServiceSeedData
{
    private readonly IServiceScopeFactory _serviceScope = serviceScope;

    public void SeedData()
    {
        using var serviceScope = _serviceScope.CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<UserManagementDbContext>();

        if (context is null)
            throw new InvalidOperationException("DbContext not found");

        var sectionGroup = new SectionGroup { Name = "Public" };
        context.SectionGroups.Add(sectionGroup);
        context.SaveChanges();
        
        List<Section> services = [];
        var declaratedServices = ConstantRetriever.GetConstants(typeof(ServiceDeclaration));

        services.AddRange(from service in declaratedServices
            where !context.Sections.Any(s => s.Code == service.Value)
            select new Section()
            {
                Name = service.Name,
                DisplayName = service.Name,
                Description = "Description for " + service.Name,
                Url = "/" + service.Name,
                Code = service.Value,
                GroupId = sectionGroup.Id,
                Type = SectionType.Service
            });

        if (services.Count == 0)
            return;

        context.Sections.AddRange(services);
        context.SaveChanges();
    }
}