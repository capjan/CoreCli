using System.Globalization;

namespace UpInfo;

internal static class UptimeOutput
{
    public const string DefaultDateFormat = "dd.MM.yyyy (HH:mm)";

    public static string FormatDate(DateTimeOffset value, string format, bool useUtc) =>
        (useUtc ? value.UtcDateTime : value.LocalDateTime).ToString(format, CultureInfo.CurrentCulture);

    public static string FormatSpan(TimeSpan value, string? customFormat, bool compact)
    {
        if (!string.IsNullOrWhiteSpace(customFormat))
            return value.ToString(customFormat, CultureInfo.CurrentCulture);

        var parts = GetParts(value);
        return compact ? string.Join(" ", parts.Select(part => part.Short)) : string.Join(", ", parts.Select(part => part.Long));
    }

    private static List<(string Long, string Short)> GetParts(TimeSpan value)
    {
        var parts = new List<(string Long, string Short)>();
        var days = (long)value.TotalDays;
        if (days > 0)
            parts.Add(($"{days} day{(days == 1 ? "" : "s")}", $"{days}d"));

        if (value.Hours > 0)
            parts.Add(($"{value.Hours} hour{(value.Hours == 1 ? "" : "s")}", $"{value.Hours}h"));

        if (value.Minutes > 0)
            parts.Add(($"{value.Minutes} minute{(value.Minutes == 1 ? "" : "s")}", $"{value.Minutes}m"));

        if (value.Seconds > 0 || parts.Count == 0)
            parts.Add(($"{value.Seconds} second{(value.Seconds == 1 ? "" : "s")}", $"{value.Seconds}s"));

        return parts;
    }
}
