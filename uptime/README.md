# uptime.exe

uptime.exe diplays the uptime of your PC since last boot.

## Reasons for uptime

1. I wan't something similar to the well known Linux "uptime" command for my Windows machine.
2. I don't need nor it want do include informations about "system load average" or "count of users".


## Usage Exampe:
```
C:\>uptime
Boot Time:    31.03.2019 (05:45)
Current Time: 07.04.2019 (01:38)
Up Time:      6 days, 19 hours, 53 minutes, 5 seconds
C:\>
```

## Options

```
Usage:
 uptime [options]

Options:
  -s, --strategy=VALUE       strategy to resolve the uptime. defaults to: wmi
                               possible values are:
                               * wmi = windows management interface
                               * tick32 = GetTickCount() Kernel function
                               * tick64 = GetTickCount64() Kernel function
                               * sw = uses the high resolution timer of the
                               stopwatch
                               * perf = Uses the "System Up Time" Performance
                               Counter
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

