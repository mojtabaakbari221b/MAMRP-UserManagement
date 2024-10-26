namespace UserManagement.Domain.Entities
{
    public class Service : IEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public long GroupId { get; set; }
        public string Description { get; set; }
    }
}
