using UserManagement.Domain.Enums;

namespace UserManagement.Domain.Entities;

public class Section : BaseEntity
{      
    public string Name { get; set; }
    public string Url { get; set; }
    public SectionGroup Group { get; set; }
    public long GroupId { get; set; }
    public string Description { get; set; }
    public SectionType Type { get; set; }
}
