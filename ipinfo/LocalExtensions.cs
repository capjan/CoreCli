using System.Net.NetworkInformation;

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace IpInfoExe;

internal static class LocalExtensions
{
    public static string WriteTable(IEnumerable<Entry> entries, bool showIpv6)
    {
        var rows = entries.ToArray();
        if (!showIpv6)
            rows = rows.Where(entry => !string.IsNullOrWhiteSpace(entry.Ipv4)).ToArray();

        if (rows.Length == 0)
            return string.Empty;

        var ipv4Width = rows.Max(entry => entry.Ipv4?.Length ?? 0);
        var ipv6Width = rows.Max(entry => entry.Ipv6?.Length ?? 0);
        var builder = new StringBuilder();

        foreach (var entry in rows)
        {
            builder.Append("    ");
            builder.Append((entry.Ipv4 ?? string.Empty).PadRight(ipv4Width));
            builder.Append("  ");

            if (showIpv6)
            {
                builder.Append((entry.Ipv6 ?? string.Empty).PadRight(ipv6Width));
                builder.Append("  ");
            }

            builder.AppendLine(entry.Name);
        }

        return builder.ToString();
    }

    public static IPAddress? FirstAddress(NetworkInterface networkInterface, AddressFamily family) =>
        networkInterface.GetIPProperties().UnicastAddresses
            .FirstOrDefault(address => address.Address.AddressFamily == family)?.Address;
}
