using UserManagement.Domain.Enums;

namespace UserManagement.Domain.Entities
{
    public class Section : IEntity
    {      
        public string Name { get; set; }
        public string Url { get; set; }
        public long GroupId { get; set; }
        public string Description { get; set; }
        public SectionType Type { get; set; }
    }
}
