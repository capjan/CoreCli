using System;
using System.Linq;
using Core.Extensions.CollectionRelated;
using Core.Parser.Arguments;

namespace build
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new OptionParser<Options>();
            if (!parser.TryParse(args, out var options))
                return;

            var versions = new MsBuildVersions();

            if (options.ListInstallations)
            {
                // print all installed msbuild  versions to stdout
                foreach (var info in versions.All)
                {
                    var v    = info.Version;
                    var path = info.FilePath;
                    Console.WriteLine($"  {v.Major,2}.{v.Minor,-2} {info.Arch}  {path}");
                }
                return;
            }

            if (options.Extra.Count == 0)
            {
                Console.WriteLine("please specify a .NET solution or project to build");
                return;
            }

            var projectsAndSolutions = options.Extra.Select(i => $"\"{i}\"").ToSeparatedString(" ");
            var configuration = options.DebugBuild ? "/p:Configuration=Debug" : "/p:Configuration=Release"; 
            var latest = versions.Latest;
            new CliRunner(latest.FilePath, $"{configuration} {projectsAndSolutions}")
                .Redirect(Console.Out);
        }
    }
}
