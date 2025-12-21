# Backend Startup Documentation
## What do you need?
- .NET Core 10.0 or higher
- Either VS Code (with the official C# extension) or Visual Studio
- For migrations you will need the Entity Framework Core Tools CLI (install via `dotnet tool install --global dotnet-ef` once you have .NET Core)

## Handling Migrations
First off to manage migrations using the `dotnet` CLI I assume that youre navigated to the `/api/`-directory (where this README is). From here you can execute all commands you can find below unless mentioned otherwise.

### 1. Adding Migrations
`dotnet ef migrations add {MigrationName} --project VisualArchitect.Api.Orchestration --startup-project VisualArchitect.Api.Orchestration --context OrchestrationDbContext --output-dir Infrastructure/Context/Migrations`

### 2. Updating the Database
`dotnet ef database update --project VisualArchitect.Api.Orchestration --startup-project VisualArchitect.Api.Orchestration --context OrchestrationDbContext`
