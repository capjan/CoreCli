using Core.Parser.Arguments;

namespace UpInfo
{
    public class Options : CliOptions
    {
        [Option("s|strategy=", "strategy to resolve the up time. defaults to: auto\npossible {value}:\n* auto = best choice for platform\n* wmi = windows management interface\n* tick32 = GetTickCount() kernel function\n* tick64 = GetTickCount64() kernel function\n* sw = stopwatch high resolution timer\n* perf = performance counter")]
        public string Strategy         { get; set; } = "auto";

        [Option("b|bootOnly", "print boot time only")]
        public bool   ShowBootTimeOnly { get; set; }

        [Option("u|upOnly", "print up time only")]
        public bool   ShowOnTimeOnly   { get; set; }

        [Option("d|dateFormat=", ".NET format string for DateTime. defaults to: \"dd.MM.yyyy (HH:mm)\" ")]
        public string DateTimeFormat { get; set; } = "dd.MM.yyyy (HH:mm)";

        [Option("t|uptimeFormat=", ".NET format string for up time TimeSpan. Defaults to: \"\"")]
        public string TimeSpanFormat { get; set; } = "";
        
        [Option("c|compact", "format the TimeSpan in compact format. Ignored if a custom TimeSpan format is set.")]
        public bool   Compact          { get; set; }

        [Option("utc", "prints Coordinated Universal Time (UTC) instead of Local Time (LT)")]
        public bool   UseUtc           { get; set; }
    }
}
