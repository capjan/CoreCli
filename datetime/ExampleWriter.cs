using System.Globalization;

namespace DatetimeExe;

internal static class ExampleWriter
{
    public static void Write(TextWriter writer, DateTimeOffset now)
    {
        writer.WriteLine("Examples:");
        Write(writer, now, "dd.MM.yyyy (HH:mm:ss)", "de-DE", false);
        Write(writer, now, "f", "de-DE", false);
        Write(writer, now, "G", "fr-FR", false);
        Write(writer, now, "G", "en-US", true);
        Write(writer, now, "T", "de-DE", true);
    }

    private static void Write(TextWriter writer, DateTimeOffset now, string format, string cultureName, bool useUtc)
    {
        var utcArgument = useUtc ? " --utc" : "";
        writer.WriteLine();
        writer.WriteLine($"> datetime --culture {cultureName}{utcArgument} --format \"{format}\"");
        writer.WriteLine(DateTimeOutput.Format(now, format, CultureInfo.GetCultureInfo(cultureName), useUtc));
    }
}
