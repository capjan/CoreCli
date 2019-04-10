using System.Collections.Generic;
using System.IO;
using Core.Parser.Arguments;

namespace UptimeExe
{
    public class CliOptions
    {
        public CliOptions(IEnumerable<string> args)
        {
            Strategy = "auto";
            DateTimeFormat =  "dd.MM.yyyy (HH:mm)";
            TimeSpanFormat = "";
            _optionSet = new OptionSet
            {
                {"s|strategy=", "strategy to resolve the uptime. defaults to: auto\npossible {value}:\n* auto = best choice for platform\n* wmi = windows management interface\n* tick32 = GetTickCount() kernel function\n* tick64 = GetTickCount64() kernel function\n* sw = stopwatch high resolution timer\n* perf = performance counter", v => Strategy = v},
                {"utc", "prints Coordinated Universal Time (UTC) instead of Local Time (LT)", v => UseUtc = v != null},
                {"b|bootOnly", "print boot time only", v=> ShowBootTimeOnly = v != null},
                {"u|upOnly", "print up time only", v=> ShowUptimeOnly = v != null},
                {"d|dateFormat=", ".NET format string for DateTime. defaults to: \"dd.MM.yyyy (HH:mm)\" ", v=> DateTimeFormat = v},
                {"t|uptimeFormat=", ".NET format string for up time TimeSpan. Defaults to: \"\"", v=> TimeSpanFormat = v},
                {"c|compact", "format the TimeSpan in compact format. Ignored if a custom TimeSpan format is set.", v => Compact = v != null},
                {"h|?|help", "display this help and exit", v=> ShowHelp                     = v != null},
                {"v|version", "show version information and exit", v=> ShowVersion          = v != null}
            };
            _optionSet.Parse(args);
        }

        public string Strategy { get; private set; }
        public bool ShowHelp { get; private set; }
        public bool ShowVersion { get; private set; }
        public bool ShowBootTimeOnly { get; private set; }
        public bool ShowUptimeOnly   { get; private set; }
        public string DateTimeFormat { get; private set; } 
        public string TimeSpanFormat { get; private set; }
        public bool Compact { get; private set; }
        public bool UseUtc { get; private set; }

        public void WriteUsage(TextWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine("Usage:");
            writer.WriteLine(" uptime [options]");
            writer.WriteLine();
            writer.WriteLine("Options:");
            _optionSet.WriteOptionDescriptions(writer);
        }

        private readonly OptionSet _optionSet;
    }
}
