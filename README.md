# CoreCli
CoreCli is a collection of programs that I missed at some point on my computer.

* [build](./build/README.md) - shortcut to build a .NET solution or project
* [datetime](./datetime/README.md) - makes it easy to format current date and time on cli
* [ipinfo](./ipinfo/README.md) - shows the public and the local ip addresses of the computer
* [upinfo](./upinfo/README.md) - shows the up time, boot time and current time


## Build Notes
For my convenience this repository is configured to be build on Windows for Windows 
and uses [Costura.Fody](https://github.com/Fody/Costura) to merge and compress the 
output to a single executable. Because Costura.Fody is targeting Windows only,
this dependency must be removed if you're building for a different OS.



