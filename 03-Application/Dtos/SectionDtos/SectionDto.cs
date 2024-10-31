namespace UserManagement.Application.Dtos.SectionDtos;

public class SectionDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public long GroupId { get; set; }
    public string Description { get; set; }
}
