# 🎯 Setup & Usage Checklist

Use this checklist to get up and running with the Settlement Survival Production Optimizer.

---

## ✅ Phase 1: Installation (First Time Only)

### Prerequisites
- [ ] **Install .NET SDK 8.0**
  - Download from: https://dotnet.microsoft.com/download/dotnet/8.0
  - OR use: `winget install Microsoft.DotNet.SDK.8`
  - Verify: Open new PowerShell and run `dotnet --version`
  - Should show: `8.0.xxx`

- [ ] **Verify Project Files**
  - Navigate to: `H:\Projects\CityHelper\SettlementSurvivalOptimizer`
  - Confirm these folders exist:
    - [ ] `Models/`
    - [ ] `Services/`
    - [ ] `Data/`
  - Confirm these files exist:
    - [ ] `Program.cs`
    - [ ] `SettlementSurvivalOptimizer.csproj`
    - [ ] `Data/buildings.json`
    - [ ] `Data/resources.json`

### Build Verification
- [ ] **Open PowerShell in project directory**
  ```powershell
  cd "H:\Projects\CityHelper\SettlementSurvivalOptimizer"
  ```

- [ ] **Restore NuGet packages**
  ```powershell
  dotnet restore
  ```
  - Should complete without errors

- [ ] **Build the project**
  ```powershell
  dotnet build
  ```
  - Should see: `Build succeeded.`
  - Should see: `0 Warning(s)` and `0 Error(s)`

- [ ] **Test run (with sample data)**
  ```powershell
  dotnet run
  ```
  - Should prompt for population
  - Should generate a report
  - Should create `optimization_report_*.txt` file

---

## ✅ Phase 2: Configuration (Customize for Your City)

### Gather Game Data
- [ ] **Open Settlement Survival**
- [ ] **Go to your Warehouse/Storage menu**
- [ ] **For each resource, note down:**
  - [ ] Current Stock
  - [ ] Used (Last Year)
  - [ ] Produced (Last Year)

### Update Resource Data
- [ ] **Open `Data/resources.json` in text editor**
  - Recommended: VS Code, Notepad++, or any text editor

- [ ] **Update EACH product with YOUR game data:**

#### Tofu
- [ ] `stock`: _________ (your current stock)
- [ ] `usedLastYear`: _________ (from game)
- [ ] `producedLastYear`: _________ (from game)
- [ ] `targetSurplus`: _________ (how much buffer you want)

#### Feast
- [ ] `stock`: _________
- [ ] `usedLastYear`: _________
- [ ] `producedLastYear`: _________
- [ ] `targetSurplus`: _________

#### Chocolate
- [ ] `stock`: _________
- [ ] `usedLastYear`: _________
- [ ] `producedLastYear`: _________
- [ ] `targetSurplus`: _________

#### Custom Gown
- [ ] `stock`: _________
- [ ] `usedLastYear`: _________
- [ ] `producedLastYear`: _________
- [ ] `targetSurplus`: _________

#### Black Tea
- [ ] `stock`: _________
- [ ] `usedLastYear`: _________
- [ ] `producedLastYear`: _________
- [ ] `targetSurplus`: _________

#### Soap
- [ ] `stock`: _________
- [ ] `usedLastYear`: _________
- [ ] `producedLastYear`: _________
- [ ] `targetSurplus`: _________

#### Popcorn
- [ ] `stock`: _________
- [ ] `usedLastYear`: _________
- [ ] `producedLastYear`: _________
- [ ] `targetSurplus`: _________

#### Whiskey
- [ ] `stock`: _________
- [ ] `usedLastYear`: _________
- [ ] `producedLastYear`: _________
- [ ] `targetSurplus`: _________

- [ ] **Save the file**

### (Optional) Adjust Buildings
- [ ] **Review `Data/buildings.json`**
- [ ] **Verify production rates match your game version**
- [ ] **Add any new buildings from mods or updates**
- [ ] **Adjust `maxWorkers` if different in your game**

---

## ✅ Phase 3: Run Optimization

### Execute
- [ ] **Open PowerShell in project directory**
  ```powershell
  cd "H:\Projects\CityHelper\SettlementSurvivalOptimizer"
  ```

- [ ] **Run the optimizer**
  ```powershell
  dotnet run
  ```

- [ ] **Enter your current population when prompted**
  - Example: `200`
  - Tip: Reserve 20-30% for non-production workers

### Review Output
- [ ] **Read the Executive Summary**
  - [ ] Note total workers required
  - [ ] Check overall efficiency
  - [ ] Review any warnings

- [ ] **Check Building Allocations**
  - [ ] Note which buildings to build
  - [ ] Note how many of each
  - [ ] Note workers per building

- [ ] **Review Resource Balance**
  - [ ] Verify final products show SURPLUS
  - [ ] Note any unexpected DEFICITS
  - [ ] Identify external resources needed

- [ ] **Read Action Items**
  - [ ] Clear list of what to build
  - [ ] Clear worker assignments

### Save Results
- [ ] **Report automatically saved as `optimization_report_*.txt`**
- [ ] **Optionally: Rename to descriptive name**
  - Example: `tofu_production_plan_nov2024.txt`

---

## ✅ Phase 4: Implementation in Game

### Phase 1 - Farms (Raw Materials)
- [ ] **Build all required farms first**
  - [ ] Soybean Farm(s): ____ buildings, ____ workers each
  - [ ] Wheat Farm(s): ____ buildings, ____ workers each
  - [ ] Corn Farm(s): ____ buildings, ____ workers each
  - [ ] Sugar Cane Farm(s): ____ buildings, ____ workers each
  - [ ] Flax Farm(s): ____ buildings, ____ workers each
  - [ ] Tomato Farm(s): ____ buildings, ____ workers each

- [ ] **Assign workers to farms**
- [ ] **Wait 1-2 game seasons for resources to accumulate**

### Phase 2 - Processing (Intermediate Goods)
- [ ] **Build processing buildings**
  - [ ] Sugar Mill(s): ____ buildings, ____ workers each
  - [ ] Textile Mill(s): ____ buildings, ____ workers each

- [ ] **Assign workers**
- [ ] **Monitor stock levels**

### Phase 3 - Production (Final Products)
- [ ] **Build production buildings**
  - [ ] Tofu Workshop(s): ____ buildings, ____ workers each
  - [ ] Feast Hall(s): ____ buildings, ____ workers each
  - [ ] Chocolate Workshop(s): ____ buildings, ____ workers each
  - [ ] Tailor Shop(s): ____ buildings, ____ workers each
  - [ ] Tea House(s): ____ buildings, ____ workers each
  - [ ] Soap Workshop(s): ____ buildings, ____ workers each
  - [ ] Popcorn Stand(s): ____ buildings, ____ workers each
  - [ ] Distillery(s): ____ buildings, ____ workers each

- [ ] **Assign workers according to report**

### Phase 4 - External Resources
- [ ] **Set up trade routes for resources with DEFICIT**
  - [ ] Cocoa: _________________
  - [ ] Tea Leaves: _____________
  - [ ] Dye: ___________________
  - [ ] Meat: __________________ (or hunting)
  - [ ] Oil: ___________________ (or production)
  - [ ] Lye: ___________________ (or production)
  - [ ] Milk: __________________ (or ranch)

---

## ✅ Phase 5: Monitor & Adjust

### After 1 Game Year
- [ ] **Check actual vs projected production**
  - [ ] Compare stock levels
  - [ ] Verify surplus/deficit matches predictions
  - [ ] Note any discrepancies

- [ ] **Update `Data/resources.json` with new numbers**
  - [ ] New stock levels
  - [ ] New "last year" consumption
  - [ ] New "last year" production

- [ ] **Re-run optimization if needed**
  ```powershell
  dotnet run
  ```

- [ ] **Adjust buildings/workers based on new report**

### Quarterly Check-In
- [ ] **Review stock levels**
  - [ ] Any products running low?
  - [ ] Any products over-producing?

- [ ] **Adjust worker assignments**
  - [ ] Increase workers if shortages
  - [ ] Decrease workers if surpluses too large

---

## ✅ Phase 6: Advanced Usage

### When to Re-Optimize
- [ ] **Population grows by >20%**
- [ ] **Add new target products**
- [ ] **Remove target products**
- [ ] **Major change in consumption patterns**
- [ ] **New buildings/technologies unlocked**
- [ ] **Game updates with balance changes**

### Interactive Mode
- [ ] **Use menu option 1** to adjust targets on-the-fly
- [ ] **Use menu option 2** to update population
- [ ] **Use menu option 3** to re-run with changes
- [ ] **Use menu option 4** to save changes

### Scenario Planning
- [ ] **Save current `resources.json` as backup**
- [ ] **Modify targets for different scenarios**
- [ ] **Run optimization**
- [ ] **Compare results**
- [ ] **Choose best scenario**

---

## 📝 Quick Reference

### Key Files
- **Edit YOUR data**: `Data/resources.json`
- **View buildings**: `Data/buildings.json`
- **Reports saved to**: `optimization_report_*.txt`

### Key Commands
```powershell
# Navigate to project
cd "H:\Projects\CityHelper\SettlementSurvivalOptimizer"

# Build project
dotnet build

# Run optimizer
dotnet run

# Clean and rebuild if issues
dotnet clean
dotnet build
```

### Target Surplus Guidelines
- **Critical products** (food, medicine): 50% of annual usage
- **Standard products** (clothing, tools): 20% of annual usage
- **Luxury products** (whiskey, chocolate): 10% of annual usage

---

## 🆘 Troubleshooting

### Build Errors
- [ ] Verify .NET SDK installed: `dotnet --version`
- [ ] Run `dotnet restore`
- [ ] Run `dotnet clean`
- [ ] Run `dotnet build`

### Runtime Errors
- [ ] Verify `Data/` folder exists
- [ ] Verify `buildings.json` and `resources.json` exist
- [ ] Check JSON syntax (use JSON validator)
- [ ] Review error message for details

### Unexpected Results
- [ ] Verify game data is accurate
- [ ] Check target surplus values are reasonable
- [ ] Review warnings in report
- [ ] Consider external resource sources

---

## ✨ Success Criteria

You've successfully set up and used the optimizer when:
- [ ] ✅ Application builds without errors
- [ ] ✅ Can run and get a report
- [ ] ✅ Report shows reasonable allocations
- [ ] ✅ Implemented recommendations in game
- [ ] ✅ Production matches projections (±10%)
- [ ] ✅ Surplus/deficit goals achieved

---

## 📚 Next Steps After Success

- [ ] **Read DESIGN.md** to understand how it works
- [ ] **Add more products** to optimize
- [ ] **Fine-tune production rates** based on experience
- [ ] **Share your configs** with other players
- [ ] **Consider contributing improvements**

---

*Check off items as you complete them. Good luck optimizing!* 🎯✨
