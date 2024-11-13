namespace UserManagement.Domain.Entities;

public class Section : BaseEntity
{      
    public string Name { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public SectionGroup? Group { get; set; }
    public long GroupId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? Code { get; set; }
    public SectionType Type { get; init; }
}
