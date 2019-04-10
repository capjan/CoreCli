using System;
using Core.Extensions.TextRelated;
using Core.Text.Formatter.Impl;

namespace DatetimeExe
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = new CliOptions(args);
            var formatter = new DefaultDateTimeFormatter();

            if (options.ShowHelp)
            {
                options.WriteHelp(Console.Out);
                return;
            }

            if (options.ShowVersion)
            {
                options.WriteVersion(Console.Out);
                return;
            }

            if (options.ShowExamples)
            {
                new ExampleWriter(formatter).WriteExamples();
                return;
            }

            try
            {
                formatter.Format = options.Format;
                formatter.UniversalTime = options.UseUtc;
                formatter.WriteLine(Console.Out);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                options.WriteHelp(Console.Out);
            }
        }
    }
}
