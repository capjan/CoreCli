using System;
using System.Management;

namespace UpInfo.UptimeResolver {
    
    /// <summary>
    /// Resolves the Up Time via Windows Management Interface (WMI), Fast and matches the result shown by the Task Manager
    /// </summary>
    public class WmiUptimeResolver : IUptimeResolver
    {
        public TimeSpan GetUptime()
        {
            return DateTime.UtcNow - GetLastBootDateTimeUtc();
        }

        public DateTime GetLastBootDateTimeUtc()
        {
            using (var management = new ManagementClass("Win32_OperatingSystem"))
            using (var store      = management.GetInstances())
            {
                foreach (var managementObject in store)
                {
                    var lastBootUpTimeObj = managementObject["LastBootUpTime"];
                    return ManagementDateTimeConverter.ToDateTime(lastBootUpTimeObj.ToString()).ToUniversalTime();
                }
            }
            return default(DateTime);
        }
    }
}