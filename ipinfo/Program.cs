using System.CommandLine;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace IpInfoExe;

internal static class Program
{
    private static async Task<int> Main(string[] args)
    {
        var ipv6Option = new Option<bool>("--ipv6", "--v6")
        {
            Description = "Display IPv6 addresses as well as IPv4 addresses."
        };
        var localOption = new Option<bool>("--local", "-l")
        {
            Description = "Only display local addresses; do not query the public IP service."
        };
        var rootCommand = new RootCommand("Show public and local IP addresses.");
        rootCommand.Options.Add(ipv6Option);
        rootCommand.Options.Add(localOption);
        rootCommand.SetAction(async (parseResult, cancellationToken) =>
        {
            try
            {
                var entries = new List<Entry>();
                if (!parseResult.GetValue(localOption))
                {
                    var publicIp = await GetPublicIpv4(cancellationToken);
                    if (publicIp is not null)
                        entries.Add(new Entry("Internet (Public)", publicIp, null));
                }

                var interfaces = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(networkInterface => networkInterface.OperationalStatus == OperationalStatus.Up)
                    .ToArray();

                AddInterfaces(entries, interfaces, NetworkInterfaceType.Ethernet);
                AddInterfaces(entries, interfaces, NetworkInterfaceType.Wireless80211);
                AddInterfaces(entries, interfaces, NetworkInterfaceType.Loopback);

                Console.Write(LocalExtensions.WriteTable(entries, parseResult.GetValue(ipv6Option)));
                return 0;
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message);
                return 1;
            }
        });

        return await rootCommand.Parse(args).InvokeAsync();
    }

    private static async Task<string?> GetPublicIpv4(CancellationToken cancellationToken)
    {
        using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };
        try
        {
            var result = await client.GetStringAsync("https://api.ipify.org", cancellationToken);
            return IPAddress.TryParse(result.Trim(), out var address)
                   && address.AddressFamily == AddressFamily.InterNetwork
                ? address.ToString()
                : null;
        }
        catch (HttpRequestException)
        {
            return null;
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return null;
        }
    }

    private static void AddInterfaces(List<Entry> entries, IEnumerable<NetworkInterface> interfaces, NetworkInterfaceType type)
    {
        foreach (var networkInterface in interfaces.Where(networkInterface => networkInterface.NetworkInterfaceType == type))
        {
            var ipv4 = LocalExtensions.FirstAddress(networkInterface, AddressFamily.InterNetwork)?.ToString();
            var ipv6 = LocalExtensions.FirstAddress(networkInterface, AddressFamily.InterNetworkV6)?.ToString();
            if (!string.IsNullOrWhiteSpace(ipv4) || !string.IsNullOrWhiteSpace(ipv6))
                entries.Add(new Entry(networkInterface.Name, ipv4, ipv6));
        }
    }

}
