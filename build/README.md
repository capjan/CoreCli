# build
searches for the latest msbuild installation and builds the given
.NET solution or project.

## Reasons for build
* I don't like to add a specific msbuild version to the system PATH variable
* I want to list all installed MsBuild versions
* I want to use the default release configuration by default.

## Options

```
Usage:

  build [options] (solution|project)

Options:
      --debug                build solution/project in debug configuration
  -l, --list                 List all MsBuild installations
  -h, -?, --help             show this help
  -v, --version              show version information and exit
```
