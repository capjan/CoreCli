using System;
using System.Text.RegularExpressions;

namespace build
{
    public class MsBuildVersionResolver
    {
        public Version Resolve(string msbuildPath)
        {
            var stdout = new CliRunner(msbuildPath, "/version")
                .ReadToEnd();

            var     matches = Regex.Matches(stdout, @"\d+(\.\d+){2,3}");
            Version result;
            Version netVersion = null;
            switch (matches.Count)
            {
                case 2:
                    result = Version.Parse(matches[0].Value);
                    break;
                case 3:
                    result     = Version.Parse(matches[0].Value);
                    netVersion = Version.Parse(matches[1].Value);
                    break;
                default:
                    throw new InvalidOperationException("unexpected return of version call");
            }

            return result;  
        }
    }
}
