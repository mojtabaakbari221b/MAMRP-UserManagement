namespace UserManagement.Domain.Entities;

public class Section : BaseEntity
{      
    public required string Name { get; set; }
    public required string DisplayName { get; set; }
    public required string Url { get; set; }
    public SectionGroup? Group { get; set; }
    public long GroupId { get; set; }
    public required string Description { get; set; }
    public string? Code { get; set; }
    public SectionType Type { get; init; }
}
