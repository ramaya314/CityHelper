# Settlement Survival Optimizer UI

A graphical user interface for optimizing production chains and worker allocation in the Settlement Survival game.

## Features

- **Resources Management**: View and edit all game resources
  - Set stock levels, production/consumption rates
  - Mark final products (target goods for your settlement)
  - Adjust target surplus values

- **Buildings Management**: View and edit all production buildings
  - Configure max workers and efficiency
  - Manage recipes for each building

- **Optimization**: Calculate optimal worker allocation
  - Input your current population
  - Get detailed breakdown of:
    - Required workers for each building
    - Resource balance (production vs consumption)
    - Surplus/deficit analysis

## Database

The application uses SQLite for persistent storage:
- Location: `%LocalAppData%\SettlementSurvivalOptimizer\optimizer.db`
- Automatically created on first run
- Sample data is seeded if database is empty

## Running the Application

```powershell
cd SettlementSurvivalOptimizer.UI
dotnet run
```

Or build and run the executable:

```powershell
dotnet build
cd bin\Debug\net8.0
.\SettlementSurvivalOptimizer.UI.exe
```

## UI Navigation

### Resources Tab
- **DataGrid**: View all resources with their properties
- **Edit Panel**: Modify selected resource
  - Name, Stock, Used/Year, Produced/Year
  - Target Surplus, Is Final Product checkbox
- **Buttons**: Add New, Save, Delete

### Buildings Tab
- **DataGrid**: View all buildings
- **Edit Panel**: Modify selected building
  - Name, Max Workers, Efficiency %
  - Recipe count (read-only)
- **Buttons**: Add New, Save, Delete

### Optimize Tab
- **Population Input**: Enter your current population
- **Run Optimization**: Calculate optimal allocation
- **Results Display**: Scrollable text output showing:
  - Building allocations with worker counts
  - Resource balance analysis
  - Summary statistics

## Data Migration

To import data from the console application's JSON files:

1. Open the console app's `Data` folder
2. Copy `buildings.json` and `resources.json`
3. In the UI, use the Resources and Buildings tabs to manually add the data
4. Or, implement a JSON import feature (future enhancement)

## Architecture

- **MVVM Pattern**: ViewModels handle business logic, Views display UI
- **Avalonia UI**: Cross-platform desktop framework
- **Entity Framework Core**: Database ORM with SQLite
- **ReactiveUI**: Reactive programming for UI interactions

## Customization

### Styling
Edit `App.axaml` to modify the dark theme colors:
- Navigation bar: `#2C2C2C`
- Background: `#1E1E1E`
- Accent: `#007ACC`
- Borders: `#3C3C3C`

### Database Schema
Modify `Models/Entities.cs` and `Data/AppDbContext.cs`, then create a migration:

```powershell
dotnet ef migrations add MigrationName
dotnet ef database update
```

## Troubleshooting

### Application won't start
- Ensure .NET 8.0 SDK is installed: `dotnet --version`
- Check for missing NuGet packages: `dotnet restore`

### Database errors
- Delete the database and restart (will reseed sample data):
  ```powershell
  Remove-Item "$env:LocalAppData\SettlementSurvivalOptimizer\optimizer.db"
  ```

### UI not responding
- Check console output for errors
- Verify database permissions (write access to LocalAppData)
