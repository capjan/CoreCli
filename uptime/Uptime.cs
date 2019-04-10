using System;
using UptimeExe.UptimeResolver;

namespace UptimeExe
{
    internal class Uptime
    {
        public DateTime Now        { get; private set; }
        public DateTime LastBoot   { get; private set; }
        public TimeSpan UptimeSpan { get; private set; }

        public Uptime(IUptimeResolver resolver)
        {
            _resolver = resolver;
            Refresh();
        }

        public void Refresh()
        {
            UptimeSpan = _resolver.GetUptime();
            Now = DateTime.UtcNow;
            LastBoot = Now - UptimeSpan;
        }

        private readonly IUptimeResolver _resolver;
    }
}
