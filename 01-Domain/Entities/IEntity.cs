using System.ComponentModel.DataAnnotations;


namespace UserManagement.Domain.Entities
{
    public class IEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime RecordDatetime { get; set; }
        public DateTime PersianRecordDatetime { get; set; }
        public long RegisteringUser { get; set; }
        public DateTime UpdateDatetime { get; set; }
        public DateTime PersianUpdateDatetime { get; set; }
        public int UpdaterUser { get; set; }
        public bool IsDeleted { get; set; }
    }
}
