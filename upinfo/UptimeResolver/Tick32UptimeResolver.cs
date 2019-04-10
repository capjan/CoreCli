using System;

namespace UpInfo.UptimeResolver {

    /// <summary>
    /// Resolves Up Time via GetTickCount() kernel function
    /// </summary>
    /// <remarks>Overflows after 49.7 days. use Tick64 to avoid this problem.</remarks>
    public class Tick32UptimeResolver : IUptimeResolver
    {
        public TimeSpan GetUptime()
        {
            return TimeSpan.FromMilliseconds(SafeNativeMethods.GetTickCount());
        }
    }
}