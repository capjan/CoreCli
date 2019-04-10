using System;
using System.Runtime.InteropServices;
using Core.ControlFlow;

namespace UptimeExe.UptimeResolver
{
    public class SecureUptimeResolver : IUptimeResolver
    {
        private SecureUptimeResolver(IUptimeResolver[] strategies)
        {
            _strategies = strategies;
        }

        public TimeSpan GetUptime()
        {
            var tryify = new Tryify<TimeSpan>();
            foreach (var strategy in _strategies)
            {
                if (tryify.TryInvoke(() => strategy.GetUptime(), out var result))
                    return result;
            }

            throw new InvalidOperationException("failed to get a valid up time");
        }

        public static IUptimeResolver Create()
        {
            IUptimeResolver[] strategies;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                strategies = new IUptimeResolver[]
                {
                    new WmiUptimeResolver(), 
                    new Tick64UptimeResolver(), 
                    new StopwatchUptimeResolver(),
                    new PerformanceCounterUptimeResolver(), 
                    new Tick32UptimeResolver()
                };
            }
            else
            {
                strategies = new IUptimeResolver[]
                {
                    new StopwatchUptimeResolver()
                };
            }
            return new SecureUptimeResolver(strategies);
        }

        private readonly IUptimeResolver[] _strategies;
    }


}
