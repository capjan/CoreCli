using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace build
{
    public class MsBuildInfo
    {
        public string FilePath { get; set; }
        public Version Version { get; set; }
        public string Arch { get; set; }

    }

    public class MsBuildVersions
    {
        public MsBuildVersions()
        {            
            Refresh();
        }

        public IReadOnlyCollection<MsBuildInfo> All => new ReadOnlyCollection<MsBuildInfo>(_all);

        public MsBuildInfo Latest => All.FirstOrDefault();

        public void Refresh()
        {
            _all = FindAllVersions();
        }

        private static MsBuildInfo[] FindAllVersions()
        {
            var installedVersions = new List<MsBuildInfo>();
            var versionResolver   = new MsBuildVersionResolver();
            var installations     = new MsBuild().FindMsBuildInstallations();
            foreach (var path in installations)
            {
                var version = versionResolver.Resolve(path);
                var arch = path.Contains("Framework64") || path.Contains("amd64") ? "x64" : "x86";
                installedVersions.Add(new MsBuildInfo {FilePath = path, Version = version, Arch = arch});
            }

            return installedVersions.OrderByDescending(i => i.Version).ThenBy(i=>i.Arch).ToArray();
        }

        private MsBuildInfo[] _all;
    }
}
