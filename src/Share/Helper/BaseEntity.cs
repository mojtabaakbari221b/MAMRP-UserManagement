namespace Share.Helper;

public abstract class BaseEntity
{
    public long Id { get; set; }
    public DateTime RecordDatetime { get; set; } = DateTime.Now;
    public DateTime PersianRecordDatetime { get; set; }
    public Guid RegisteringUser { get; set; }
    public DateTime UpdateDatetime { get; set; }
    public DateTime PersianUpdateDatetime { get; set; }
    public Guid UpdaterUser { get; set; }
    public bool IsActive { get; set; }
}
