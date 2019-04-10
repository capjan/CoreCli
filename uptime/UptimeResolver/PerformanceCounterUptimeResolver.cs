using System;
using System.Diagnostics;

namespace UptimeExe.UptimeResolver {

    /// <summary>
    /// Resolves the Up Time via performance counter
    /// </summary>
    /// <remarks>
    /// This strategy is quite accurate, but it's by a very slow one
    /// </remarks>
    public class PerformanceCounterUptimeResolver : IUptimeResolver
    {
        public TimeSpan GetUptime()
        {
            using (var uptime = new PerformanceCounter("System", "System Up Time")) {
                uptime.NextValue();       //Call this an extra time before reading its value
                return TimeSpan.FromSeconds(uptime.NextValue());
            }
        }
    }
}