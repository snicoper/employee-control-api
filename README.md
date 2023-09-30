# Employee Control

`dotnet tool install -g dotnet-ef`

## Migración

```bash
cd src/WebApi

dotnet ef migrations add Initial -p ../Infrastructure/Infrastructure.csproj  -c ApplicationDbContext  -o ../Infrastructure/Data/Migrations/ApplicationMigrations

dotnet ef database update -c ApplicationDbContext
```

## Secrets

```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=postgres;Password=Password44!;Server=localhost;Port=5432;Database=EmployeeControl;Integrated Security=true;Pooling=true;"
  }
}
```
