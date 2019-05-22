using System;
using System.Globalization;
using Core.Extensions.CollectionRelated;
using Core.Extensions.TextRelated;
using Core.Parser.Arguments;
using Core.Text.Formatter.Impl;

namespace DatetimeExe
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var parser = new OptionParser<Options>();
            if (!parser.TryParse(args, out var options))
                return;

            try
            {

                var provider = new CultureInfo(options.CultureTwoLetterCode);
            
                var formatter = new DefaultDateTimeFormatter(formatProvider: provider);

                if (options.ShowExamples)
                {
                    new ExampleWriter(formatter).WriteExamples();
                    return;
                }

                if (options.Extra.Count > 1) throw new InvalidOperationException($"unexpected count of arguments: {options.Extra.ToSeparatedString()}");

                if (options.Extra.Count != 0)
                    formatter.Format = options.Extra[0];

                formatter.UniversalTime = options.UseUtc;
                formatter.WriteLine(Console.Out);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                parser.WriteUsage();
            }
        }
    }
}
