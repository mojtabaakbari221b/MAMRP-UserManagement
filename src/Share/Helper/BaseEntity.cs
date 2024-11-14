namespace Share.Helper;

public abstract class BaseEntity
{
    public long Id { get; set; }
    public DateTime RecordDatetime { get; init; } = DateTime.Now;
    public PersianDateTime PersianRecordDatetime { get; init; } = PersianDateTime.Now;
    public Guid RegisteringUser { get; set; }
    public DateTime UpdateDatetime { get; set; } = DateTime.Now;
    public PersianDateTime PersianUpdateDatetime { get; private set; } = PersianDateTime.Now;
    public Guid UpdaterUser { get; set; }
    public bool IsActive { get; set; } = true;


    public void SetUpdateDatetime()
    {
        UpdateDatetime = DateTime.Now;
        PersianUpdateDatetime = PersianDateTime.Now;
    }
}
