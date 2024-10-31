using UserManagement.Domain.Enums;

namespace UserManagement.Domain.Entities
{
    public class SectionGroup : IEntity
    {
        public string Name { get; set; }
        public SectionType Type { get; set; }
    }
}
