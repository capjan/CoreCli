using System.Globalization;
using IpInfoExe;
using UpInfo;
using Xunit;
using DatetimeExe;

namespace CoreCli.Tests;

public class DateTimeOutputTests
{
    [Fact]
    public void FormatsUtcWithRequestedCulture()
    {
        var value = new DateTimeOffset(2026, 10, 5, 12, 30, 0, TimeSpan.FromHours(2));

        var output = DateTimeOutput.Format(value, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, true);

        Assert.Equal("05.10.2026 10:30", output);
    }

    [Fact]
    public void MapsUsCultureAliasToUsEnglish()
    {
        Assert.Equal("en-US", DateTimeOutput.GetCulture("us").Name);
    }
}

public class UptimeOutputTests
{
    [Fact]
    public void FormatsUptimeAsHumanReadableParts()
    {
        var output = UptimeOutput.FormatSpan(TimeSpan.FromDays(3) + TimeSpan.FromHours(4) + TimeSpan.FromMinutes(5) + TimeSpan.FromSeconds(6), null, false);

        Assert.Equal("3 days, 4 hours, 5 minutes, 6 seconds", output);
    }

    [Fact]
    public void FormatsUptimeCompactly()
    {
        var output = UptimeOutput.FormatSpan(TimeSpan.FromDays(3) + TimeSpan.FromHours(4) + TimeSpan.FromMinutes(5) + TimeSpan.FromSeconds(6), null, true);

        Assert.Equal("3d 4h 5m 6s", output);
    }
}

public class IpInfoTableTests
{
    [Fact]
    public void EmptyAddressListRendersWithoutThrowing()
    {
        Assert.Equal(string.Empty, LocalExtensions.WriteTable([], false));
    }

    [Fact]
    public void SkipsIpv6OnlyRowsWhenIpv6WasNotRequested()
    {
        var entries = new[]
        {
            new Entry("Ethernet", "192.0.2.10", "2001:db8::1"),
            new Entry("IPv6 tunnel", null, "2001:db8::2")
        };

        var output = LocalExtensions.WriteTable(entries, false);

        Assert.Contains("192.0.2.10  Ethernet", output);
        Assert.DoesNotContain("IPv6 tunnel", output);
    }

    [Fact]
    public void IncludesIpv6WhenRequested()
    {
        var entries = new[] { new Entry("Ethernet", "192.0.2.10", "2001:db8::1") };

        var output = LocalExtensions.WriteTable(entries, true);

        Assert.Contains("192.0.2.10", output);
        Assert.Contains("2001:db8::1", output);
        Assert.Contains("Ethernet", output);
    }
}
