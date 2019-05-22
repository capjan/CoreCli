using System;
using Core.Extensions.TextRelated;
using Core.Parser.Arguments;
using Core.Text.Formatter.Impl;
using UpInfo.UptimeResolver;

namespace UpInfo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var parser = new OptionParser<Options>();
            if (!parser.TryParse(args, out var options))
                return;

            var dateFormatter = new DefaultDateTimeFormatter();
            var timeSpanFormatter = new DefaultTimeSpanFormatter();

            try
            {
                var resolver = GetStrategy(options.Strategy);
                var uptime = new Uptime(resolver);

                dateFormatter.Format           = options.DateTimeFormat;
                dateFormatter.UniversalTime    = options.UseUtc;
                timeSpanFormatter.CustomFormat = options.TimeSpanFormat;
                timeSpanFormatter.Compact      = options.Compact;

                if (options.ShowBootTimeOnly)
                {
                    dateFormatter.WriteLine(uptime.LastBoot, Console.Out);
                    return;
                }

                if (options.ShowOnTimeOnly)
                {
                    timeSpanFormatter.WriteLine(uptime.UptimeSpan, Console.Out);
                    return;
                }

                Console.Write("Boot Time:    ");
                dateFormatter.WriteLine(uptime.LastBoot, Console.Out);
                Console.Write("Current Time: ");
                dateFormatter.WriteLine(uptime.Now, Console.Out);
                Console.Write("Up Time:      ");
                timeSpanFormatter.WriteLine(uptime.UptimeSpan, Console.Out);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                parser.WriteUsage();
            }
        }

        private static IUptimeResolver GetStrategy(string value)
        {
            switch (value.ToLower())
            {
                case "auto":
                    return SecureUptimeResolver.Create();
                case "wmi":
                    return new WmiUptimeResolver();
                case "tick32":
                    return new Tick32UptimeResolver();
                case "tick":
                case "tick64":
                    return new Tick64UptimeResolver();
                case "sw":
                case "stopwatch":
                    return new StopwatchUptimeResolver();
                case "perf":
                    return new PerformanceCounterUptimeResolver();
                default:
                    throw new ArgumentException($"invalid up time resolve strategy: \"{value}\" use: auto, wmi, tick32, tick64, sw or perf");
            }
        }
    }
}
