# datetime

Format the current date and time with a .NET date/time format string.

```text
Usage: datetime [options] [format]

Arguments:
  format                 .NET date/time format string. Default: dd.MM.yyyy (HH:mm)

Options:
  -f, --format <format>  Set a .NET date/time format string.
  -c, --culture <culture>
                         Set a culture name, for example en-US or de-DE.
      --utc              Format Coordinated Universal Time instead of local time.
      --examples         Show usage examples.
  -v, --version          Show version information.
  -?, -h, --help         Show help and usage information.
```

Examples:

```sh
datetime --format "dd.MM.yyyy (HH:mm:ss)" --culture de-DE
datetime --format "G" --culture en-US --utc
datetime "MMMM dd, yyyy"
```

The `us` culture alias maps to `en-US` for compatibility. Date and time format strings follow the .NET `DateTime` formatting rules.
