namespace Share.Extensions;

public static class DateTimeExtensions
{
    public static DateTime ToPersianDateTime(this DateTime dateTime)
    {
        PersianCalendar persianCalendar = new();

        var persianYear = persianCalendar.GetYear(dateTime);
        var persianMonth = persianCalendar.GetMonth(dateTime);
        var persianDay = persianCalendar.GetDayOfMonth(dateTime);

        var hour = persianCalendar.GetHour(dateTime);
        var minute = persianCalendar.GetMinute(dateTime);
        var second = persianCalendar.GetSecond(dateTime);

        return new DateTime(
            persianYear, persianMonth, persianDay, hour, minute, second,
            new PersianCalendar()
        );
    }
}