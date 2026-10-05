# upinfo

Show system uptime, boot time, and current time using .NET's platform-independent uptime counter.

```text
Usage: upinfo [options]

Options:
  -b, --boot-only       Print the boot time only. (--bootOnly is also accepted.)
  -u, --up-only         Print the uptime only. (--upOnly is also accepted.)
  -d, --date-format     Set a .NET date format. Default: dd.MM.yyyy (HH:mm)
  -t, --uptime-format   Set a custom .NET TimeSpan format.
  -c, --compact         Print the uptime in compact form.
      --utc             Print times in Coordinated Universal Time.
  -v, --version         Show version information.
  -?, -h, --help        Show help and usage information.
```

Examples:

```sh
upinfo
upinfo --up-only --compact
upinfo --boot-only --utc --date-format "yyyy-MM-dd HH:mm:ss"
```
