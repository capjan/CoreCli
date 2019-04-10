using System;
using System.Reflection;
using Core.Extensions.ReflectionRelated;
using Core.Extensions.TextRelated;
using Core.Reflection;
using Core.Text.Formatter.Impl;
using UptimeExe.UptimeResolver;

namespace UptimeExe
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = new CliOptions(args);
            var dateFormatter = new DefaultDateTimeFormatter();
            var timeSpanFormatter = new DefaultTimeSpanFormatter();

            try
            {
                if (options.ShowHelp)
                {
                    options.WriteUsage(Console.Out);
                    return;
                }

                if (options.ShowVersion)
                {
                    var asmInfo = new AssemblyInfo(Assembly.GetExecutingAssembly());
                    Console.WriteLine($" {asmInfo.Product} Version {asmInfo.GetBestMatchingVersion()}");
                    return;
                }

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

                if (options.ShowUptimeOnly)
                {
                    timeSpanFormatter.WriteLine(uptime.UptimeSpan, Console.Out);
                    return;
                }

                Console.Write($"Boot Time:    ");
                dateFormatter.WriteLine(uptime.LastBoot, Console.Out);
                Console.Write($"Current Time: ");
                dateFormatter.WriteLine(uptime.Now, Console.Out);
                Console.Write($"Up Time:      ");
                timeSpanFormatter.WriteLine(uptime.UptimeSpan, Console.Out);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                options.WriteUsage(Console.Out);
            }
        }

        private static IUptimeResolver GetStrategy(string value)
        {
            switch (value.ToLower())
            {
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
                    throw new ArgumentException($"invalid uptime resolve strategy: \"{value}\" use: wmi, tick32, tick64, sw or perf");
            }
        }
    }
}
