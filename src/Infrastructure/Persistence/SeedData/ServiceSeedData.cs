namespace UserManagement.Infrastructure.Persistence.SeedData;

public sealed class ServiceSeedData(IServiceScopeFactory serviceScope) : IServiceSeedData
{
    private readonly IServiceScopeFactory _serviceScope = serviceScope;

    public void SeedData()
    {
        using var serviceScope = _serviceScope.CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<UserManagementDbContext>();
        using var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

        var sectionGroup = new SectionGroup { Name = "Public" };
        if (!context!.SectionGroups.Any(s => s.Name == "Public"))
        {
            context.SectionGroups.Add(sectionGroup);
            context.SaveChanges();
        }

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


        if (userManager!.Users.Any(u => u.UserName == "admin"))
        {
            return;
        }

        User newUser = new()
        {
            FirstName = "Admin",
            FamilyName = "admin",
            UserName = "admin",
            Email = "admin@Mam.com",
        };
        var result = userManager.CreateAsync(newUser, "@@Admin1@@").Result;


        var user = context.Users.First(s => s.FirstName == "admin");

        var userClaims = context.Sections.AsQueryable()
            .Where(s => s.Type == SectionType.Service)
            .Select(s => new UserClaim()
            {
                ClaimType = s.Name,
                ClaimValue = s.Code,
                SectionId = s.Id,
                UserId = user.Id
            })
            .ToList();

        context.UserClaims.AddRange(userClaims);
        context.SaveChanges();
    }
}