using System.Globalization;
using Core.Parser.Arguments;

namespace DatetimeExe
{
    public class Options : CliOptions
    {
        [Option("examples", "show usage examples")]
        public bool ShowExamples { get; set; }

        [Option("c|culture=", "sets the used rules and localization as two letter language code (ISO 639). defaults to current local culture ")]
        public string CultureTwoLetterCode { get; set; } = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

        [Option("utc", "prints Coordinated Universal Time (UTC) instead of Local Time (LT)")]
        public bool UseUtc { get; set; }
    }
}
