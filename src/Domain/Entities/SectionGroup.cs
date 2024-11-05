namespace UserManagement.Domain.Entities;

public class SectionGroup : BaseEntity
{
    public required string Name { get; set; }
    public SectionType Type { get; init; }
}
