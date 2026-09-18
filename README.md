# .NET Core Sandbox

This repository contains personal learning exercises for .NET and Entity Framework Core. Most examples follow *C# 12 and .NET 8* by Mark J. Price, using the Polish edition published by Helion. `EFMigrationsOverview` is based on the official .NET documentation.

The projects currently target .NET 10. Build or run an individual application from its project directory with `dotnet build` or `dotnet run`. Run the test project with `dotnet test`.

## Learning Projects

### LINQ with Objects

`LinqWithObjects` contains basic LINQ exercises using in-memory data.

### LINQ with EF Core

`LinqWithEFCore` contains LINQ exercises using the Northwind database.

### Code First

`CodeFirstExample` shows a simple academy database with students and courses.

## EFCoreLearn

`EFCoreLearn` is a larger Northwind exercise split into two projects:

- `EFCore` is the executable console application. It contains interactive queries, relationship loading, filtering, projections, and product create, update, and delete commands.
- `Northwind.Models` is the shared class library containing the `Category` and `Product` entities.

The `EFCore` project also contains an `AutoGenModel` directory with classes generated from the database schema. `Northwind4SQLite.sql` is the SQLite setup script. Run the application with:

```bash
cd EFCoreLearn/EFCore
dotnet run
```

## EFMigrationsOverview

`EFMigrationsOverview` is a focused example based on the official .NET documentation. It uses a `Blog` entity and `BlogContext` with SQLite. On startup it applies pending EF Core migrations, inserts a first blog when the database is empty, and reads the blogs back without tracking.

The `Migrations` directory contains the initial schema migration, a migration adding the blog creation timestamp, and the model snapshot. Run it with:

```bash
cd EFMigrationsOverview
dotnet run
```

## PracticalNetApps

`PracticalNetApps` is the largest example in the repository. It builds several application front ends around one Northwind SQLite data layer:

- `Northwind.EntityModels.Sqlite` contains entity classes for customers, orders, products, suppliers, employees, territories, and related tables.
- `Northwind.DataContext.Sqlite` configures `NorthwindContext` and provides registration helpers for ASP.NET Core applications.
- `Northwind.WebApi` exposes customer operations through controllers and a repository. Development mode includes Swagger/OpenAPI, HTTP logging, and JSON/XML formatters.
- `Northwind.Web` is a server-rendered Razor Pages application with supplier and order views.
- `Northwind.Blazor` is an interactive Blazor Server application with customer list, details, create, edit, and delete pages.
- `Northwind.Blazor.Services` contains the service interface and implementation used by the Blazor UI.
- `Northwind.UnitTests` contains xUnit tests for the shared Northwind data layer.

The shared database script is `PracticalNetApps/Northwind4SQLite.sql`. Run a web application from its own directory, for example:

```bash
cd PracticalNetApps/Northwind.WebApi
dotnet run
```

Run the automated tests with:

```bash
cd PracticalNetApps/Northwind.UnitTests
dotnet test
```
