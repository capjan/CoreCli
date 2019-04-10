using System;

namespace UptimeExe.UptimeResolver
{
    /// <summary>
    /// Interface to resolve the up time of the current machine.
    /// </summary>
    public interface IUptimeResolver
    {
        TimeSpan GetUptime();
    }
}