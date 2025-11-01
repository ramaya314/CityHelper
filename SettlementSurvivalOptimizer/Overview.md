# 🎮 Settlement Survival Production Optimizer

**A sophisticated .NET application that optimizes production chains and worker allocation for Settlement Survival**

---

## 🚀 What This Does

Takes your target products (like Tofu, Whiskey, Chocolate) and tells you:
- ✅ How many of each building type to build
- ✅ How many workers to assign to each building
- ✅ What your resource balance will be
- ✅ Where potential bottlenecks exist

**All calculated automatically using backward-chaining through your entire production chain!**

---

## ⚡ Quick Example

**You input:**
```
Target: 1,300 Tofu/year with +100 surplus
Population: 200 workers available
```

**You get:**
```
BUILD:
  3× Tofu Workshop → 9 workers total (3 per building)
  1× Soybean Farm → 4 workers total

RESULT:
  Production: 1,350 Tofu/year
  Consumption: 1,200 Tofu/year  
  Surplus: +150 Tofu/year ✓
```

---

## 📦 What's Included

### Core Application
- **ProductionOptimizer**: Main calculation engine
- **DataLoader**: Manages configuration
- **ReportGenerator**: Creates readable reports
- **Interactive Mode**: Adjust targets on-the-fly

### Pre-Configured Data
- **8 Final Products**: Tofu, Feast, Chocolate, Custom Gown, Black Tea, Soap, Popcorn, Whiskey
- **6 Crop Farms**: Soybean, Wheat, Tomato, Sugar Cane, Flax, Corn
- **16 Building Types**: Complete with recipes and production rates

### Documentation
- **README.md**: Complete feature documentation
- **QUICKSTART.md**: Fast track to first optimization
- **INSTALLATION.md**: .NET SDK setup guide
- **DESIGN.md**: Technical architecture details
- **SAMPLE_REPORT.md**: Example output explained
- **PROJECT_SUMMARY.md**: This overview

---

## 🎯 Key Features

### 1️⃣ Backward-Chaining Analysis
Starts with what you want to produce and works backward:
```
Custom Gown → needs Fabric → needs Flax → needs Farm
```
Automatically calculates requirements at each step!

### 2️⃣ Smart Worker Allocation
- Considers partial-staffing penalties
- Optimizes building count vs workers per building
- Maximizes overall efficiency
- Respects population limits

### 3️⃣ Stock Intelligence
- Warns when stock is low (< 25% annual usage)
- Adjusts production to rebuild buffers
- Projects annual balance
- Prevents cascading shortages

### 4️⃣ Complete Chain Visibility
See your entire production network:
```
FINAL PRODUCTS → INTERMEDIATES → RAW MATERIALS
     ↓                ↓               ↓
  Whiskey ← Wheat + Water ← Farm + Well
```

---

## 📋 How to Use

### Step 1: Install Prerequisites
```powershell
# Download .NET SDK from:
# https://dotnet.microsoft.com/download/dotnet/8.0

# Or use Windows Package Manager:
winget install Microsoft.DotNet.SDK.8
```

### Step 2: Navigate to Project
```powershell
cd "H:\Projects\CityHelper\SettlementSurvivalOptimizer"
```

### Step 3: Update Your Data
Edit `Data/resources.json` with YOUR game statistics:
```json
{
  "name": "Tofu",
  "stock": 500,              ← Current stock from game
  "usedLastYear": 1200,      ← From warehouse statistics
  "producedLastYear": 1300,  ← From warehouse statistics
  "targetSurplus": 100,      ← How much buffer you want
  "isFinalProduct": true     ← Mark products to optimize
}
```

### Step 4: Run Optimization
```powershell
dotnet run
```

### Step 5: Implement & Profit!
Follow the building allocations and worker assignments from the generated report.

---

## 📊 Sample Output

```
═══════════════════════════════════════════════════════════════════
EXECUTIVE SUMMARY
─────────────────────────────────────────────────────────────────
Total Workers Required: 147 / 200 available
Population Utilization: 73.5%
Overall Efficiency: 85.2%
Total Buildings Needed: 28

BUILDING ALLOCATIONS
─────────────────────────────────────────────────────────────────
Tofu Workshop
  Buildings: 3
  Workers per Building: 3.0
  Total Workers: 9
  Expected Production: 1,350 Tofu/year

RESOURCE BALANCE
─────────────────────────────────────────────────────────────────
★ Tofu          SURPLUS      +150 units/year
★ Chocolate     SURPLUS      +100 units/year
★ Whiskey       SURPLUS       +50 units/year
  Wheat         DEFICIT    -1,525 units/year ← Need farm/trade

ACTION ITEMS
─────────────────────────────────────────────────────────────────
  → Build 3 Tofu Workshops with 9 total workers
  → Build 2 Chocolate Workshops with 6 total workers
  → Build 1 Soybean Farm with 4 total workers
  ...
═══════════════════════════════════════════════════════════════════
```

---

## 🎓 Documentation Guide

| Document | Purpose | Read When |
|----------|---------|-----------|
| **README.md** | Complete documentation | Understanding all features |
| **QUICKSTART.md** | Fast-track usage guide | Ready to use immediately |
| **INSTALLATION.md** | .NET SDK setup | First time setup |
| **DESIGN.md** | Architecture details | Want to understand internals |
| **SAMPLE_REPORT.md** | Example output | Learning to read reports |
| **PROJECT_SUMMARY.md** | High-level overview | Getting started |

---

## 💡 Why This System Is Special

### Problem: Manual Balancing is Hard
- Multiple production tiers (raw → intermediate → final)
- Resources shared across chains
- Partial staffing affects efficiency
- Stock shortages cascade
- Constant micromanagement needed

### Solution: Automated Optimization
- ✅ Calculates entire chain automatically
- ✅ Optimizes worker allocation
- ✅ Projects resource balance
- ✅ Provides clear action items
- ✅ Minimizes ongoing maintenance

---

## 🔮 Future Possibilities

Consider extending with:
- GUI interface (WPF/Blazor/Avalonia)
- Multi-producer optimization
- Seasonal variation modeling
- Technology upgrade effects
- Trade route integration
- Scenario comparison
- Excel/CSV export
- Real-time game data import

---

## 📂 Project Files Reference

### Code Files
```
Models/Resource.cs           - Resource data model
Models/Building.cs           - Building definition
Models/Recipe.cs             - Production recipe
Services/ProductionOptimizer.cs - Core algorithm
Services/ReportGenerator.cs  - Output formatting
Program.cs                   - Main application
```

### Configuration Files
```
Data/buildings.json   - All buildings & recipes
Data/resources.json   - Your game data & targets
```

### Documentation Files
```
README.md            - Full documentation
QUICKSTART.md        - Quick usage guide
INSTALLATION.md      - Setup instructions
DESIGN.md            - Technical details
SAMPLE_REPORT.md     - Example output
PROJECT_SUMMARY.md   - This file
```

---

## 🎯 Best Practices

1. **Accurate Data**: Use real in-game statistics for best results
2. **Reasonable Buffers**: Set target surplus to 10-20% of usage
3. **Iterate**: Re-run after implementation to fine-tune
4. **Monitor**: Watch for warnings about population or stock
5. **Update Regularly**: Re-optimize when city changes significantly

---

## ⚙️ System Requirements

- **OS**: Windows, Linux, or macOS
- **.NET**: 8.0 SDK or later
- **RAM**: 512 MB minimum
- **Disk**: 5 MB for application

---

## 🤝 Getting Help

1. **Installation Issues**: See `INSTALLATION.md`
2. **Usage Questions**: See `QUICKSTART.md`
3. **Understanding Output**: See `SAMPLE_REPORT.md`
4. **Technical Details**: See `DESIGN.md`
5. **Feature Reference**: See `README.md`

---

## ✨ Summary

You have a complete, production-ready system that:

- 🎯 **Optimizes** production chains automatically
- 👷 **Calculates** optimal worker allocations
- 🏗️ **Recommends** building counts
- 📊 **Projects** resource balances
- ⚡ **Minimizes** micromanagement
- 🔧 **Customizes** to your city's needs

**All powered by YOUR game data and designed to save you time!**

---

## 🚀 Next Steps

1. ✅ Install .NET SDK (see INSTALLATION.md)
2. ✅ Build project: `dotnet build`
3. ✅ Update game data in `Data/resources.json`
4. ✅ Run: `dotnet run`
5. ✅ Review report
6. ✅ Implement in game
7. ✅ Track results & iterate

---

## 📜 License

Free to use and modify for personal Settlement Survival gameplay.

---

## 🎮 Happy Optimizing!

**May your settlements thrive and your production chains flourish!** 🏘️✨

---

*Built with ❤️ for Settlement Survival players who value efficiency over micromanagement*
