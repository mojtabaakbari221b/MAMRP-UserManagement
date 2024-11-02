using UserManagement.Domain.Enums;

namespace UserManagement.Domain.Entities;

public class SectionGroup : BaseEntity
{
    public string Name { get; set; }
    public SectionType Type { get; set; }
}
