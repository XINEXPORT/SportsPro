# Developer Instructions

## Connecting MS SQL Server to your Program
Go to Program.cs </br>
Include your database connection string in the AddDbContext. The connection string to your DB is "SportsPro"
```
// Configure the DbContext
builder.Services.AddDbContext<SportsProContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SportsPro"))
);
```
## Command that adds seed
```
dotnet ef migration add Initial
dotnet ef migration add <table>
```
## Command that updates the database to the last seedlings
```
dotnet ef database update
```
## Command that updates the database to the specified seed
```
dotnet ef migration update Initial
```
## Command that removes the last seed
```
dotnet ef migrations remove
```
## Command that drops the database
```
dotnet ef database drop
```
## Command that formats the entire code base
```
dotnet csharpier .
```
