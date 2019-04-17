# regex
Exposes the .NET regular expression engine to the command line.

## Options

```
Usage:

  regex [option]... pattern (file|directory)...

Arguments:
  pattern           The search pattern as .NET Regular Expression (RegEx).
  file              File to operate.
  directory         Directory to operate. (Must end with a directory separator)

Options:
  -R, --replace=VALUE        replacement Pattern (regex)
  -c, --case-sensitive       enables case-sensitive behavior - btw. disables
                               the by default enabled ignore-case option
  -f, --filter=VALUE         wildcard based file filter (default *.*)
                               e.g. *.txt
  -r, --recursive            progress all subdirectories
      --offset-width=VALUE   output-formatting:
                               set the count of characters used for the offset
                               column (default 6)
  -v, --verbose              show additional information
  -V, --version              show version information
  -h, --help                 shows this help
```

## Usage Examples

Find "Hello" in all *.txt files in this folder and all subfolders
```
C:>regex --recursive --filter *.txt Hello ./
```



