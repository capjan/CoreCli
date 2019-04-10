# datetime.exe
makes it easy to format date and time on the command line.
Essentially this tool exposes the .NET DateTime class to the command line.

## Options

```
Usage:

  datetime [options]... [format]

Arguments:
  format                     .NET DateTime Format. Defaults to "dd.MM.yyyy (HH:mm)"

Options:
      --utc                  prints Coordinated Universal Time (UTC) instead
                               of Local Time (LT)
  -c, --culture=CULTURE      sets the used CULTURE rules and localization as
                               two letter language code (ISO 639). defaults to
                               current local culture (now: en)
  -h, -?, --help             show this help
      --examples             show usage examples
  -v, --version              show version information and exit
```

## Usage Examples
```
C:\>datetime --format "dd.MM.yyyy (HH:mm:ss)" --culture us
03.04.2019 (15:01:54)

C:\>datetime --format "f" --culture de
Mittwoch, 3. April 2019 15:01

C:\>datetime --format "G" --culture fr
03/04/2019 15:01:54

C:\>datetime --format "G" --culture us --utc
04/03/2019 13:01:54

C:\>datetime --format "T" --culture de --utc
13:01:54
```



