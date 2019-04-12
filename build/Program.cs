using System;
using System.Linq;
using System.Reflection;
using Core.Extensions.CollectionRelated;
using Core.Extensions.ReflectionRelated;
using Core.Reflection;

namespace build
{
    class Program
    {
        static void Main(string[] args)
        {

            var options = new CliOptions(args);

            if (options.ShowHelp)
            {
                options.WriteUsage(Console.Out);
                return;
            }
            
            if (options.ShowVersion)
            {
                options.WriteVersion(Console.Out);
                return;
            }

            var versions = new MsBuildVersions();



            if (options.ListInstallations)
            {
                // Print all installed versions to stdout
                foreach (var info in versions.All)
                {
                    var v    = info.Version;
                    var path = info.FilePath;
                    Console.WriteLine($"  {v.Major,2}.{v.Minor,-2} {info.Arch}  {path}");
                }
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
