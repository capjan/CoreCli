# ipinfo

Show IPv4 addresses for the public internet connection and active local Ethernet, Wi-Fi, and loopback interfaces. The public address is resolved through `https://api.ipify.org`; use `--local` to avoid the network request.

```text
Usage: ipinfo [options]

Options:
  --ipv6, --v6          Display IPv6 addresses as well as IPv4 addresses.
  -l, --local           Only display local addresses; do not query the public IP service.
  -v, --version         Show version information.
  -?, -h, --help        Show help and usage information.
```

Examples:

```sh
ipinfo
ipinfo --ipv6
ipinfo --local
```
