# build
Searches for the latest MSBuild installation and builds the given
.NET solution or project.

## Reasons for build
* I want a foolproof easy shortcut to build a .NET Solution/Project via command line
* I don't like to add a specific MSBuild version to the system PATH variable
* I want to list all installed MSBuild versions
* I want to use the default release configuration by default

## Options

```
Usage:

  build [options] (solution|project)

Options:
      --debug                build solution/project in debug configuration
  -l, --list                 List all MSBuild installations
  -h, -?, --help             show this help
  -v, --version              show version information and exit
```

## Examples

build a project

```
C:>build yourNiceProject.csproj
Microsoft (R) Build Engine version 15.9.21+g9802d43bc3 for .NET Framework
Copyright (C) Microsoft Corporation. All rights reserved.

Build started 12.04.2019 15:50:51.

*** HERE COMES THE WALL OF TEXT FROM MSBUILD ***

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:00.26
```

List all installed MSBuild installations
```
C:>build --list
  15.9  x64  C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\amd64\MSBuild.exe
  15.9  x86  C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe
  14.0  x64  C:\Program Files (x86)\MSBuild\14.0\Bin\amd64\MSBuild.exe
  14.0  x86  C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe
   4.7  x64  C:\WINDOWS\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe
   4.7  x86  C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe
   3.5  x64  C:\WINDOWS\Microsoft.NET\Framework64\v3.5\MSBuild.exe
   3.5  x86  C:\WINDOWS\Microsoft.NET\Framework\v3.5\MSBuild.exe
   2.0  x64  C:\WINDOWS\Microsoft.NET\Framework64\v2.0.50727\MSBuild.exe
   2.0  x86  C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\MSBuild.exe
```

