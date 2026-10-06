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

## NuGet Trusted Publishing

The release workflow publishes the three tool packages with NuGet.org Trusted Publishing through GitHub Actions OIDC. No long-lived NuGet API key is required.

Configure Trusted Publishing once for the NuGet.org account that owns the packages:

1. In the NuGet.org account that owns the packages, add a GitHub Trusted Publishing policy for owner `capjan`, repository `CoreCli`, workflow file `release.yml`, and environment `nuget`.
2. Add the NuGet.org profile name (not the email address or an API key) as the GitHub Actions secret `NUGET_USER`.
3. Push a semantic version tag, such as `v2.0.1`, to run the release workflow.

The first successful publish also finalizes NuGet.org's repository ownership binding for the policy.

## License

MIT. See [LICENSE](./LICENSE).
