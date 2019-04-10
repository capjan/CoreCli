using System.Collections.Generic;
using System.IO;
using Core.Parser.Arguments;

namespace IpInfoExe
{
    public class CliOptions
    {

        public CliOptions(IEnumerable<string> args)
        {
            _optionSet = new OptionSet
            {
                {"v6|ipv6", "display also IPv6 results", v => ShowIpv6 = v != null},
                {"l|local", "restrict to local ip info", v=> LocalOnly = v != null},
                {"h|?|help", "display this help and exit", v => ShowHelp            = v != null},
                {"v|version", "show version information and exit", v => ShowVersion = v != null}
            };
            _optionSet.Parse(args);
        }

        public bool ShowIpv6    { get; private set; }
        public bool ShowHelp    { get; private set; }
        public bool ShowVersion { get; private set; }
        public bool LocalOnly   { get; private set; }

        public void WriteUsage(TextWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine("Usage:");
            writer.WriteLine(" ipinfo [options]");
            writer.WriteLine();
            writer.WriteLine("Options:");
            _optionSet.WriteOptionDescriptions(writer);
        }

        private readonly OptionSet _optionSet;
    }
}
