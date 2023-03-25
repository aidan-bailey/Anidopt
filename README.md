# Anidopt
Classifieds web-app for animal adoption (WIP)

## Usage

Make sure `dotnet-ef` is installed

``` sh
dotnet tool update --global dotnet-ef
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