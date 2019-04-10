using System;
using System.Diagnostics;

namespace UptimeExe.UptimeResolver {

    /// <summary>
    /// Resolves the Up Time via Stopwatch Frequency. 
    /// </summary>
    /// <remarks>
    /// This method relies on the system having a HPET so ensure Stopwatch.IsHighResolution is true before using it.
    /// </remarks>
    public class StopwatchUptimeResolver : IUptimeResolver
    {
        public TimeSpan GetUptime()
        {
            var ticks  = Stopwatch.GetTimestamp();
            var uptime = (double)ticks / Stopwatch.Frequency;
            return TimeSpan.FromSeconds(uptime);
        }
    }
}