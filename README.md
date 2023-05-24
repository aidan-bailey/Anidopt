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

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published
by the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.

Copyright (C) 2023 Aidan Bailey