namespace UserManagement.Application.Dtos
{
    public class CreateMenuDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public long GroupId { get; set; }
        public string Description { get; set; }
    }
}
