using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Core.Extensions.TextRelated;
using Core.Text.Formatter;
using Core.Text.Formatter.Impl;

namespace DatetimeExe
{
    internal class ExampleWriter
    {
        public ExampleWriter(
            IDateTimeFormatter formatter = default,
            TextWriter writer = default,
            string exeName = null)
        {
            _exeName = exeName ?? Assembly.GetExecutingAssembly().GetName().Name;
            _writer = writer ?? Console.Out;
            _formatter = formatter ?? new DefaultDateTimeFormatter("g");
        }

        public void WriteExamples()
        {
            _writer.WriteLine("Examples:");
            WriteExample("dd.MM.yyyy (HH:mm:ss)", "us", false);
            WriteExample("f", "de", false);
            WriteExample("G", "fr", false);
            WriteExample("G", "us", true);
            WriteExample("T", "de", true);
        }

        public void WriteExample(string format, string cultureName, bool useUtc)
        {
            _writer.WriteLine();
            _formatter.FormatProvider = new CultureInfo(cultureName);
            _formatter.Format = format;
            _formatter.UniversalTime = !useUtc;
            _writer.Write($"> {_exeName} --culture {cultureName}");
            if (useUtc) _writer.Write(" --utc");
            _writer.WriteLine($" \"{_formatter.Format}\"");
            _formatter.Write(_writer);
            _writer.WriteLine();
        }

        private readonly TextWriter _writer;
        private readonly IDateTimeFormatter _formatter;
        private readonly string _exeName;
    }
}
