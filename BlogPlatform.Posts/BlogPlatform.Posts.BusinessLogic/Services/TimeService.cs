using BlogPlatform.Posts.BusinessLogic.Services.Contracts;
using Microsoft.Extensions.Localization;
using System.Text;

namespace BlogPlatform.Posts.BusinessLogic.Services;

public class TimeService : ITimeService
{
    private readonly IStringLocalizer<TimeService> _locale;

    public TimeService(IStringLocalizer<TimeService> locale)
    {
        _locale = locale;
    }

    public string ConvertToLocalRelativeString(DateTime dateTime)
    {
        TimeSpan dateDiff = DateTime.Now.Subtract(dateTime);

        if (dateDiff.TotalSeconds < 0)
        {
            return _locale["Later"];
        }

        if (dateDiff.TotalSeconds < 60)
        {
            return _locale["JustNow"];
        }

        int minuteDiff = (int)dateDiff.TotalMinutes;

        if (minuteDiff < 60)
        {
            return ConstructRelativeString(minuteDiff, "Minute");
        }

        int hourDiff = (int)dateDiff.TotalHours;

        if (hourDiff < 24)
        {
            return ConstructRelativeString(hourDiff, "Hour");
        }

        int dayDiff = (int)dateDiff.TotalDays;

        if (dayDiff < 7)
        {
            return ConstructRelativeString(dayDiff, "Day");
        }

        int weekDiff = dayDiff / 7;

        if (weekDiff < 5)
        {
            return ConstructRelativeString(weekDiff, "Week");
        }

        int monthDiff = weekDiff / 4;

        if (monthDiff < 12)
        {
            return ConstructRelativeString(monthDiff, "Month");
        }

        return ConstructRelativeString(monthDiff / 12, "Year");
    }

    private string ConstructRelativeString(int value, string unit)
    {
        string unitKey;

        if (value > 11 && value % 10 == 1)
        {
            unitKey = $"{unit}sOne";
        }
        else
        {
            unitKey = value switch
            {
                1 => unit,
                >= 2 and <= 4 => $"Couple{unit}s",
                _ => $"{unit}s"
            };
        }

        StringBuilder stringBuilder = new();
        return stringBuilder.Append(value)
            .Append(' ')
            .Append(_locale[unitKey])
            .Append(' ')
            .Append(_locale["Ago"])
            .ToString();
    }
}
