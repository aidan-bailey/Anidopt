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

The database will be created if it does not exist.