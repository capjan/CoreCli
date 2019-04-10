# upinfo.exe

upinfo.exe diplays the up time of your computer since last boot.
To prevent a name collusion with "uptime" on Linux and OS X computers, this program 
was renamed from uptime to upinfo.

## Reasons for upinfo

1. I wan't something similar to the well known Linux "uptime" command for my Windows machine.
2. I don't need nor it want do include informations about "system load average" or "count of users".


## Usage Exampe:
```
C:\>upinfo
Boot Time:    31.03.2019 (05:45)
Current Time: 07.04.2019 (01:38)
Up Time:      6 days, 19 hours, 53 minutes, 5 seconds
C:\>
```

## Options

```
Usage:
 upinfo [options]

Options:
  -s, --strategy=value       strategy to resolve the up time. defaults to:
                               auto
                               possible value:
                               * auto = best choice for platform
                               * wmi = windows management interface
                               * tick32 = GetTickCount() kernel function
                               * tick64 = GetTickCount64() kernel function
                               * sw = stopwatch high resolution timer
                               * perf = performance counter
      --utc                  prints Coordinated Universal Time (UTC) instead
                               of Local Time (LT)
  -b, --bootOnly             print boot time only
  -u, --upOnly               print up time only
  -d, --dateFormat=VALUE     .NET format string for DateTime. defaults to:
                               "dd.MM.yyyy (HH:mm)"
  -t, --uptimeFormat=VALUE   .NET format string for up time TimeSpan.
                               Defaults to: ""
  -c, --compact              format the TimeSpan in compact format. Ignored
                               if a custom TimeSpan format is set.
  -h, -?, --help             display this help and exit
  -v, --version              show version information and exit
```

