using System.ComponentModel.DataAnnotations;

namespace UserManagement.Application.Dtos
{
    public class CreateMenuDto
    {
        [MaxLength(2 , ErrorMessage ="حداکثر 50 کاراکتر")]
        public string Name { get; set; }
        public string Url { get; set; }
        public long GroupId { get; set; }
        public string Description { get; set; }
    }
}
