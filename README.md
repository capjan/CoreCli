# CoreCli

CoreCli is a small collection of command-line utilities:

* [datetime](./datetime/README.md) formats the current date and time.
* [ipinfo](./ipinfo/README.md) shows public and local IP addresses.
* [upinfo](./upinfo/README.md) shows system uptime, boot time, and current time.

The tools target .NET 10 and are distributed as NuGet global tools. Each tool has only `System.CommandLine` as a runtime package dependency.

## Install

Install only the commands you need:

```sh
dotnet tool install --global CoreCli.DateTime
dotnet tool install --global CoreCli.IpInfo
dotnet tool install --global CoreCli.UpInfo
```

## Build and test

```sh
dotnet test CoreCli.sln --configuration Release
dotnet pack CoreCli.sln --configuration Release --output artifacts/packages
```

## License

MIT. See [LICENSE](./LICENSE).
