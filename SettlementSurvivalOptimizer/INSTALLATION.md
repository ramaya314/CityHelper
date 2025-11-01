# Settlement Survival Optimizer - Installation Guide

## Prerequisites

Before running this application, you need to install the .NET SDK.

### Installing .NET 8.0 SDK

#### Windows

1. **Download .NET 8.0 SDK**:
   - Visit: https://dotnet.microsoft.com/download/dotnet/8.0
   - Click "Download .NET SDK x64" (for 64-bit Windows)

2. **Run the installer**:
   - Double-click the downloaded `.exe` file
   - Follow the installation wizard
   - Accept the license terms
   - Complete the installation

3. **Verify installation**:
   - Open a new PowerShell window
   - Run: `dotnet --version`
   - You should see something like `8.0.xxx`

#### Alternative: Install via winget (Windows Package Manager)

If you have Windows 10 version 1809 or later:

```powershell
winget install Microsoft.DotNet.SDK.8
```

## Building the Application

Once .NET SDK is installed:

1. **Open PowerShell in the project directory**:
   ```powershell
   cd "H:\Projects\CityHelper\SettlementSurvivalOptimizer"
   ```

2. **Restore NuGet packages**:
   ```powershell
   dotnet restore
   ```

3. **Build the project**:
   ```powershell
   dotnet build
   ```

   You should see:
   ```
   Build succeeded.
       0 Warning(s)
       0 Error(s)
   ```

4. **Run the application**:
   ```powershell
   dotnet run
   ```

## Alternative: Use Visual Studio

If you prefer a GUI:

1. **Download Visual Studio 2022 Community** (free):
   - Visit: https://visualstudio.microsoft.com/downloads/
   - Download "Visual Studio 2022 Community"

2. **During installation**, select:
   - ".NET desktop development" workload
   - ".NET 8.0 Runtime" (should be included)

3. **Open the solution**:
   - Launch Visual Studio
   - File → Open → Project/Solution
   - Navigate to: `H:\Projects\CityHelper\SettlementSurvivalOptimizer`
   - Open: `SettlementSurvivalOptimizer.sln`

4. **Build and run**:
   - Press `F5` or click the green "Start" button
   - Or: Build → Build Solution, then Debug → Start Without Debugging

## Alternative: Use Visual Studio Code

For a lighter-weight editor:

1. **Install VS Code**:
   - Visit: https://code.visualstudio.com/
   - Download and install

2. **Install C# Dev Kit extension**:
   - Open VS Code
   - Go to Extensions (Ctrl+Shift+X)
   - Search for "C# Dev Kit"
   - Click Install

3. **Install .NET SDK** (as described above)

4. **Open the project**:
   - File → Open Folder
   - Select: `H:\Projects\CityHelper\SettlementSurvivalOptimizer`

5. **Run**:
   - Open terminal in VS Code (Ctrl+`)
   - Run: `dotnet run`

## Troubleshooting

### "dotnet is not recognized"

**Problem**: .NET SDK not installed or not in PATH

**Solution**:
1. Install .NET SDK (see above)
2. Restart PowerShell/terminal after installation
3. If still not working, restart your computer

### "The project file could not be loaded"

**Problem**: Corrupted project file

**Solution**:
1. Verify all files were created correctly
2. Re-download or re-create the project
3. Check file encoding (should be UTF-8)

### Build errors about missing packages

**Problem**: NuGet packages not restored

**Solution**:
```powershell
dotnet restore
dotnet clean
dotnet build
```

### "Could not find Data/buildings.json"

**Problem**: Application can't find data files

**Solution**:
1. Verify `Data` folder exists in project root
2. Verify `buildings.json` and `resources.json` exist
3. Run from project root directory

## System Requirements

- **OS**: Windows 10/11, Linux, or macOS
- **RAM**: 512 MB minimum
- **Disk Space**: 200 MB for .NET SDK + 5 MB for application
- **.NET**: Version 8.0 or later

## Quick Start After Installation

1. Navigate to project:
   ```powershell
   cd "H:\Projects\CityHelper\SettlementSurvivalOptimizer"
   ```

2. Run the application:
   ```powershell
   dotnet run
   ```

3. Follow the on-screen prompts

4. View the generated report

5. Check `QUICKSTART.md` for detailed usage instructions

## Need Help?

- **Installation issues**: Check Microsoft's official .NET documentation
- **Build errors**: Review error messages and check file paths
- **Usage questions**: See `README.md` and `QUICKSTART.md`
- **System design**: Review `DESIGN.md`

## Next Steps

Once successfully installed and running:

1. ✅ Verify sample data runs correctly
2. ✅ Update `Data/resources.json` with YOUR game data
3. ✅ Run optimization for your city
4. ✅ Implement recommendations in game
5. ✅ Track results and re-optimize as needed

---

**Enjoy optimizing your Settlement Survival city!** 🏘️✨
