namespace UserManagement.Domain.Entities
{
    public class User
    {
        public long MemberId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }
    }
}
