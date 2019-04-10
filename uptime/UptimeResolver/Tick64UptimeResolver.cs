using System;

namespace UptimeExe.UptimeResolver {

    /// <summary>
    /// Resolves Up Time via GetTickCount64 Kernel function. 
    /// </summary>
    /// <remarks>
    /// This method is very fast, but is introduced with Windows Vista and will fail at later Windows versions
    /// </remarks>
    public class Tick64UptimeResolver : IUptimeResolver
    {
        public TimeSpan GetUptime()
        {
            return TimeSpan.FromMilliseconds(SafeNativeMethods.GetTickCount64());
        }
    }
}