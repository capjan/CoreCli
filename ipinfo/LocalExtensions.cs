using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace IpInfoExe
{
    public static class LocalExtensions
    {

        public static IPAddress IpV4Address(this NetworkInterface value)
        {
            return value.FirstByFamily(AddressFamily.InterNetwork);
        }

        public static IPAddress IpV6Address(this NetworkInterface value)
        {
            return value.FirstByFamily(AddressFamily.InterNetworkV6);
        }

        public static IPAddress FirstByFamily(this NetworkInterface value, AddressFamily family)
        {
            return value
                   .GetIPProperties()
                   .UnicastAddresses
                   .FirstOrDefault(i=>i.Address.AddressFamily == family)?.Address;
        }

        public static void WriteByType(this IEnumerable<NetworkInterface> allInterfaces, NetworkInterfaceType type, AddressFamily family)
        {
            var filtered = allInterfaces.Where(i => i.NetworkInterfaceType == type && i.FirstByFamily(family) != null).ToArray();
            var addressList = filtered.Select(i => i.FirstByFamily(family)).ToArray();

            for (var i = 0; i < filtered.Length; i++)
            {
                var adr = addressList[i].ToString().PadRight(20);
                var name = filtered[i].Name;
                Console.WriteLine($"{adr} {name}");
            }
        }

        public static void Add(this IList<Entry> list, string name, string ipv4, string ipv6)
        {
            list.Add(new Entry()
            {
                Name = name ?? "",
                Ipv4 = ipv4 ?? "",
                Ipv6 = ipv6 ?? ""
            });
        }

        public static void AddRange(this IList<Entry> list, IEnumerable<NetworkInterface> values)
        {
            foreach (var item in values)
            {
                var name = item.Name;
                var ipv4 = item.IpV4Address()?.ToString();
                var ipv6 = item.IpV6Address()?.ToString();
                // skip entries that we can't render properly
                if (string.IsNullOrWhiteSpace(ipv4) && string.IsNullOrWhiteSpace(ipv6)) continue;
                list.Add(name, ipv4, ipv6);
            }
        }

        public static void WriteTable(this IList<Entry> list, TextWriter writer, bool showIpv6)
        {
            var col1Len = list.Select(i => i.Ipv4.Length).Max();
            var col2Len = list.Select(i => i.Ipv6.Length).Max();
            var indent = new string(' ', 4);
            foreach (var entry in list)
            {
                var col1 = entry.Ipv4.PadRight(col1Len);
                var col2 = entry.Ipv6.PadRight(col2Len);
                writer.Write(indent);
                if (showIpv6)
                {
                    writer.WriteLine($"{col1}  {col2}  {entry.Name}");
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(entry.Ipv4)) continue;
                    writer.WriteLine($"{col1}  {entry.Name}");
                }
            }
        }

    }
}
