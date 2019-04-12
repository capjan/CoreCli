using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Core.Extensions.ReflectionRelated;
using Core.Parser.Arguments;
using Core.Reflection;

namespace build
{
    public class CliOptions
    {
        public CliOptions(IEnumerable<string> args)
        {
            _optionSet = new OptionSet
            {
                {"debug", "build solution/project in debug configuration", v => DebugBuild = v != null},                
                {"l|list", "List all MsBuild installations", v => ListInstallations = v != null},                
                {"h|?|help", "show this help", v => ShowHelp = v != null},                
                {"v|version", "show version information and exit", v => ShowVersion = v != null}
            };
            try
            {
                Extra = _optionSet.Parse(args).ToArray();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                ShowHelp = true;
            }
        }

        public bool            DebugBuild        { get; set; }
        public bool            ListInstallations { get; private set; }
        public bool            ShowHelp          { get; private set; }
        public bool            ShowVersion       { get; private set; }


        public string[]        Extra          { get; }

        public void WriteUsage(TextWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine("Usage:");
            writer.WriteLine(" build [options] (solution|project)");
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
    }
}
