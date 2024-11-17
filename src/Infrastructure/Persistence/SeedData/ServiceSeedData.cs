namespace UserManagement.Infrastructure.Persistence.SeedData;

public sealed class ServiceSeedData(IServiceScopeFactory serviceScope) : IServiceSeedData
{
    private readonly IServiceScopeFactory _serviceScope = serviceScope;

    public void SeedData()
    {
        using var serviceScope = _serviceScope.CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<UserManagementDbContext>();
        using var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
        var optionBearer = serviceScope.ServiceProvider.GetService<IOptions<TokenOption>>()!.Value.BearerTokenOption;
        

        var sectionGroup = new SectionGroup { Name = "Public" };
        context!.SectionGroups.Add(sectionGroup);
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

        if (services.Count != 0)
        {
            context.Sections.AddRange(services);
            context.SaveChanges();
        }
        
        if (userManager!.Users.Any(u => u.FirstName == "Admin"))
            return;

        User user = new()
        {
            FirstName = "Admin",
            FamilyName = "admin",
            UserName = "admin",
            Email = "admin@Mam.com",
        };

        var result = userManager.CreateAsync(user, "@@Admin1@@").Result;
        if (!result.Succeeded)
        {
            return;
        }

        var claims = context.Sections.AsQueryable()
            .Select(rc => new Claim(rc.Name, rc.Url, ClaimValueTypes.String, optionBearer.Issuer))
            .ToList();
        
        userManager.AddClaimsAsync(user, claims);
    }
}