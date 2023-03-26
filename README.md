# Anidopt
Classifieds web-app for animal adoption (WIP)

## Requirements

- [.NET 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [MS SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Usage

Make sure `dotnet-ef` is installed

``` sh
dotnet tool install --global dotnet-ef
```

Create a json file called *appsettings.json* in the `Anidopt` directory containing an `AnidoptContext` connection string, e.g.

```json
{
  "ConnectionStrings": {
    "AnidoptContext": "Server=(localdb)\\MSSQLLocalDB;Database=_CHANGE_ME;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

A database will be built with the connection string if it does not exist.

Use the typical `dotnet` cli commands for the rest...

**Build**

```sh
dotnet build
```

**Clean**

```sh
dotnet clean
```

**Run**

```sh
dotnet watch
```

...blah blah blah...

## License

This code is being written with the purpose of doing good things for free.
Anidopt currently rides under [GPLv2](https://github.com/aidanjbailey/Anidopt/blob/master/LICENSE).
If it's good enough for Linus it's good enough for me.