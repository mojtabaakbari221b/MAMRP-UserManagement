namespace UserManagement.Domain.Entities
{
    public class Role : IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Service> AllowedServices { get; set; }
        public IList<Menu> AllowedMenus { get; set; }
    }
}
