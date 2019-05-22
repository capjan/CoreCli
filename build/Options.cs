using Core.Parser.Arguments;

namespace build
{
    public class Options : CliOptions
    {
        [Option("debug", "build solution/project in debug configuration")]
        public bool DebugBuild        { get; set; }

        [Option("l|list", "List all MsBuild installations")]
        public bool ListInstallations { get; set; }
    }
}
