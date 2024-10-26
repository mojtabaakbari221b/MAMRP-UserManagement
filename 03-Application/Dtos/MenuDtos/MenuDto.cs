namespace UserManagement.Application.Dtos.MenuDtos
{
    public class MenuDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public long GroupId { get; set; }
        public string Description { get; set; }
    }
}
