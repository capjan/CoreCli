using System.CommandLine;

namespace UpInfo;

internal static class Program
{
    private static int Main(string[] args)
    {
        var bootOnlyOption = new Option<bool>("--boot-only", "-b")
        {
            Description = "Print the boot time only."
        };
        bootOnlyOption.Aliases.Add("--bootOnly");
        var upOnlyOption = new Option<bool>("--up-only", "-u")
        {
            Description = "Print the uptime only."
        };
        upOnlyOption.Aliases.Add("--upOnly");
        var dateFormatOption = new Option<string>("--date-format", "-d")
        {
            Description = $"Set the .NET date format. Defaults to {UptimeOutput.DefaultDateFormat}."
        };
        dateFormatOption.Aliases.Add("--dateFormat");
        var uptimeFormatOption = new Option<string?>("--uptime-format", "-t")
        {
            Description = "Set a custom .NET TimeSpan format."
        };
        uptimeFormatOption.Aliases.Add("--uptimeFormat");
        var compactOption = new Option<bool>("--compact", "-c")
        {
            Description = "Print the uptime in compact form."
        };
        var utcOption = new Option<bool>("--utc")
        {
            Description = "Print times in Coordinated Universal Time."
        };
        var rootCommand = new RootCommand("Show system uptime, boot time, and current time.");
        rootCommand.Options.Add(bootOnlyOption);
        rootCommand.Options.Add(upOnlyOption);
        rootCommand.Options.Add(dateFormatOption);
        rootCommand.Options.Add(uptimeFormatOption);
        rootCommand.Options.Add(compactOption);
        rootCommand.Options.Add(utcOption);
        rootCommand.SetAction(parseResult =>
        {
            if (parseResult.GetValue(bootOnlyOption) && parseResult.GetValue(upOnlyOption))
            {
                Console.Error.WriteLine("Choose either --boot-only or --up-only.");
                return 2;
            }

            try
            {
                var now = DateTimeOffset.Now;
                var uptime = TimeSpan.FromMilliseconds(Environment.TickCount64);
                var bootTime = now - uptime;
                var dateFormat = parseResult.GetValue(dateFormatOption) ?? UptimeOutput.DefaultDateFormat;
                var spanFormat = parseResult.GetValue(uptimeFormatOption);
                var compact = parseResult.GetValue(compactOption);
                var utc = parseResult.GetValue(utcOption);

                if (parseResult.GetValue(bootOnlyOption))
                {
                    Console.WriteLine(UptimeOutput.FormatDate(bootTime, dateFormat, utc));
                    return 0;
                }

                if (parseResult.GetValue(upOnlyOption))
                {
                    Console.WriteLine(UptimeOutput.FormatSpan(uptime, spanFormat, compact));
                    return 0;
                }

                Console.WriteLine($"Boot Time:    {UptimeOutput.FormatDate(bootTime, dateFormat, utc)}");
                Console.WriteLine($"Current Time: {UptimeOutput.FormatDate(now, dateFormat, utc)}");
                Console.WriteLine($"Up Time:      {UptimeOutput.FormatSpan(uptime, spanFormat, compact)}");
                return 0;
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
