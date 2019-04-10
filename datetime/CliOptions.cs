using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Core.Extensions.CollectionRelated;
using Core.Extensions.ReflectionRelated;
using Core.Parser.Arguments;
using Core.Reflection;

namespace DatetimeExe
{
    public class CliOptions
    {
        public CliOptions(IEnumerable<string> args)
        {
            FormatProvider = _currentCulture;
            _optionSet = new OptionSet
            {
                {"utc", "prints Coordinated Universal Time (UTC) instead of Local Time (LT)", v => UseUtc = v != null},
                {"c|culture=", $"sets the used {{CULTURE}} rules and localization as two letter language code (ISO 639). defaults to current local culture (now: {_currentCulture.TwoLetterISOLanguageName})", v => FormatProvider = new CultureInfo(v.Trim())},
                {"h|?|help", "show this help", v => ShowHelp = v != null},
                {"examples", "show usage examples", v=> ShowExamples = v != null},
                {"v|version", "show version information and exit", v => ShowVersion = v != null}
            };
            try
            {
                Extra = _optionSet.Parse(args).ToArray();
                if (Extra.Length > 1) throw new InvalidOperationException($"unexpected count of arguments: {Extra.ToSeparatedString()}");
                Format = (Extra.Length == 1) ? Format = Extra[0] : "dd.MM.yyyy (HH:mm)";
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                ShowHelp = true;
            }
        }

        public bool            ShowHelp       { get; private set; }
        public bool            ShowVersion    { get; private set; }
        public bool            ShowExamples   { get; private set; }
        public bool            UseUtc         { get; private set; }
        public IFormatProvider FormatProvider { get; private set; }
        public string          Format         { get; }
        public string[]        Extra          { get; }

        public void WriteHelp(TextWriter writer)
        {
            WriteVersion(writer);
            writer.WriteLine("Usage:\n");
            writer.WriteLine("  datetime [options]... [format]\n");
            writer.WriteLine("Arguments:");
            writer.WriteLine($"  {"format",-27}.NET DateTime Format. Defaults to \"dd.MM.yyyy (HH:mm)\"");
            writer.WriteLine();
            writer.WriteLine("Options:");
            _optionSet.WriteOptionDescriptions(writer);
        }

        public void WriteVersion(TextWriter writer)
        {
            var asmInfo = new AssemblyInfo(Assembly.GetExecutingAssembly());
            writer.WriteLine($"{asmInfo.Product} {asmInfo.GetVersionSummary()}");
        }

        private readonly OptionSet _optionSet;
        private readonly CultureInfo _currentCulture = CultureInfo.CurrentCulture;
    }
}
