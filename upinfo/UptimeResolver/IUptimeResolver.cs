using System;

namespace UpInfo.UptimeResolver
{
    /// <summary>
    /// Interface to resolve the up time of the current machine.
    /// </summary>
    public interface IUptimeResolver
    {
        TimeSpan GetUptime();
    }
}