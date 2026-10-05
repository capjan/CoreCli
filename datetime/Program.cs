using System.CommandLine;
using System.Globalization;

namespace DatetimeExe;

internal static class Program
{
    private static int Main(string[] args)
    {
        var formatArgument = new Argument<string?>("format")
        {
            Description = "Optional .NET date and time format string.",
            Arity = ArgumentArity.ZeroOrOne
        };
        var formatOption = new Option<string?>("--format", "-f")
        {
            Description = "Set a .NET date and time format string."
        };
        var cultureOption = new Option<string?>("--culture", "-c")
        {
            Description = "Set a culture name, for example en-US or de-DE."
        };
        var utcOption = new Option<bool>("--utc")
        {
            Description = "Format Coordinated Universal Time instead of local time."
        };
        var examplesOption = new Option<bool>("--examples")
        {
            Description = "Show usage examples."
        };
        var rootCommand = new RootCommand("Format the current date and time.");
        rootCommand.Arguments.Add(formatArgument);
        rootCommand.Options.Add(formatOption);
        rootCommand.Options.Add(cultureOption);
        rootCommand.Options.Add(utcOption);
        rootCommand.Options.Add(examplesOption);
        rootCommand.SetAction(parseResult =>
        {
            try
            {
                var format = parseResult.GetValue(formatOption)
                             ?? parseResult.GetValue(formatArgument)
                             ?? DateTimeOutput.DefaultFormat;
                var culture = DateTimeOutput.GetCulture(parseResult.GetValue(cultureOption));
                var useUtc = parseResult.GetValue(utcOption);

                if (parseResult.GetValue(examplesOption))
                {
                    ExampleWriter.Write(Console.Out, DateTimeOffset.Now);
                    return 0;
                }

                Console.WriteLine(DateTimeOutput.Format(DateTimeOffset.Now, format, culture, useUtc));
                return 0;
            }
            catch (ArgumentException exception)
            {
                Console.Error.WriteLine(exception.Message);
                return 2;
            }
            catch (FormatException exception)
            {
                Console.Error.WriteLine(exception.Message);
                return 2;
            }
        });

        return rootCommand.Parse(args).Invoke();
    }

}

internal static class DateTimeOutput
{
    public const string DefaultFormat = "dd.MM.yyyy (HH:mm)";

    public static CultureInfo GetCulture(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return CultureInfo.CurrentCulture;

        if (name.Equals("us", StringComparison.OrdinalIgnoreCase))
            name = "en-US";

        return CultureInfo.GetCultureInfo(name);
    }

    public static string Format(DateTimeOffset now, string format, CultureInfo culture, bool useUtc)
    {
        var value = useUtc ? now.UtcDateTime : now.LocalDateTime;
        return value.ToString(format, culture);
    }
}
