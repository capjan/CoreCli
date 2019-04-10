using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using Core.Extensions.NetRelated;
using Core.Extensions.ReflectionRelated;
using Core.Net.Impl;
using Core.Reflection;

namespace IpInfoExe
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = new CliOptions(args);

            try
            {
                if (options.ShowHelp)
                {
                    options.WriteUsage(Console.Out);
                    return;
                }

                if (options.ShowVersion)
                {
                    var asmInfo = new AssemblyInfo(Assembly.GetExecutingAssembly());
                    Console.WriteLine($" {asmInfo.Product} Version {asmInfo.GetBestMatchingVersion()}");
                    return;
                }

                var table = new List<Entry>();
                if (!options.LocalOnly && new DefaultPublicIpResolver().TryResolve(out var publicIp))
                        table.Add("Internet (Public)", publicIp, null);

                var allInterfaces = NetworkInterface
                                    .GetAllNetworkInterfaces()
                                    .Where(i => i.OperationalStatus == OperationalStatus.Up)
                                    .ToArray();

                table.AddRange(allInterfaces.Where(i => i.NetworkInterfaceType == NetworkInterfaceType.Ethernet));
                table.AddRange(allInterfaces.Where(i => i.NetworkInterfaceType == NetworkInterfaceType.Wireless80211));
                table.AddRange(allInterfaces.Where(i => i.NetworkInterfaceType == NetworkInterfaceType.Loopback));
                
                table.WriteTable(Console.Out, options.ShowIpv6);

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                options.WriteUsage(Console.Out);
            }
        }

    }
}
