using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace build
{
    public class MsBuild
    {
        public MsBuild()
        {            
            _msbuildLookupPaths = CreateMsLookupDirectories();
        }

        private static string[] CreateMsLookupDirectories()
        {
            var result = new List<string>();

            // MS Build bundled with .NET
            var windowsDir      = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            var microsoftNetDir = Path.Combine(windowsDir, "Microsoft.NET");
            var frameworkDir    = Path.Combine(microsoftNetDir, "Framework");
            var framework64Dir  = Path.Combine(microsoftNetDir, "Framework64");
            result.Add(frameworkDir);
            result.Add(framework64Dir);

            // MS Build Standalone
            var programFilesX86Dir     = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            var msbuildInstallationDir = Path.Combine(programFilesX86Dir, "MSBuild");
            result.Add(msbuildInstallationDir);            
            
            // MS Build bundled with Visual Studio 
            var editions = new string[] {"Community", "Professional", "Enterprise"};
            foreach (var edition in editions)
            {
                result.Add(Path.Combine(programFilesX86Dir, "Microsoft Visual Studio", "2017", edition, "MSBuild"));
            }
                       
            return result.ToArray();
        }

        public IEnumerable<string> FindMsBuildInstallations()
        {
            var result = new List<string>();
            var searchPattern = "msbuild.exe";

            foreach (var lookupPath in _msbuildLookupPaths)            
                if (Directory.Exists(lookupPath))                
                    result.AddRange(Directory.GetFiles(lookupPath, searchPattern, SearchOption.AllDirectories));

            return result;
        }
      

        private readonly string[] _msbuildLookupPaths;

    }
}
