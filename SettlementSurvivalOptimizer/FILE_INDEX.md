# 📁 Project File Index

Complete reference for all files in the Settlement Survival Production Optimizer project.

---

## 📂 Root Directory

### Documentation Files
| File | Purpose | Read When |
|------|---------|-----------|
| **Overview.md** | High-level project overview | Getting started, first time |
| **README.md** | Complete feature documentation | Understanding all capabilities |
| **QUICKSTART.md** | Fast-track usage guide | Ready to use immediately |
| **INSTALLATION.md** | .NET SDK setup instructions | First time setup |
| **DESIGN.md** | Technical architecture | Want to understand internals |
| **PROJECT_SUMMARY.md** | Comprehensive project summary | Understanding what was built |
| **SAMPLE_REPORT.md** | Example optimization output | Learning to interpret results |
| **CHECKLIST.md** | Step-by-step setup checklist | Following implementation process |

### Project Files
| File | Purpose |
|------|---------|
| **SettlementSurvivalOptimizer.sln** | Visual Studio solution file |
| **SettlementSurvivalOptimizer.csproj** | Project configuration & dependencies |
| **Program.cs** | Main application entry point |
| **.gitignore** | Git ignore rules for build artifacts |

---

## 📂 Models/ Directory

Core data models representing game entities.

| File | Contains | Purpose |
|------|----------|---------|
| **Resource.cs** | `Resource` class | Represents game resources/products with stats |
| **Recipe.cs** | `Recipe` class | Defines production transformations |
| **Building.cs** | `Building` class | Represents production facilities |
| **ProductionChain.cs** | `ProductionChain` class | Complete production graph structure |
| **BuildingAllocation.cs** | `BuildingAllocation` class | Optimization result per building |
| **OptimizationResult.cs** | `OptimizationResult` class | Complete optimization output |

### Model Relationships
```
ProductionChain
  ├─ Buildings (Dictionary<string, Building>)
  │   └─ Recipes (List<Recipe>)
  │       ├─ Inputs (Dictionary<string, double>)
  │       └─ Outputs (Dictionary<string, double>)
  └─ Resources (Dictionary<string, Resource>)

OptimizationResult
  ├─ BuildingAllocations (List<BuildingAllocation>)
  ├─ ResourceBalance (Dictionary<string, double>)
  └─ Warnings (List<string>)
```

---

## 📂 Services/ Directory

Core business logic and optimization algorithms.

| File | Contains | Purpose |
|------|----------|---------|
| **ProductionOptimizer.cs** | `ProductionOptimizer` class | Main optimization engine |
| **DataLoader.cs** | `DataLoader` class | JSON configuration loader/saver |
| **ReportGenerator.cs** | `ReportGenerator` class | Human-readable report generation |

### Service Methods

#### ProductionOptimizer
- `Optimize()` - Main optimization entry point
- `CalculateResourceDemands()` - Backward chaining demand calculation
- `AllocateProductionCapacity()` - Building/worker allocation
- `CalculateResourceBalance()` - Production vs consumption analysis
- `CalculateOverallEfficiency()` - Efficiency scoring

#### DataLoader
- `LoadProductionChain()` - Load buildings and recipes from JSON
- `LoadResources()` - Load resource data from JSON
- `SaveResources()` - Save updated resource data

#### ReportGenerator
- `GenerateDetailedReport()` - Full optimization report
- `GenerateQuickSummary()` - Condensed summary

---

## 📂 Data/ Directory

JSON configuration files for buildings and resources.

| File | Contains | Customizable | Purpose |
|------|----------|--------------|---------|
| **buildings.json** | Building definitions & recipes | Yes | Defines all production capabilities |
| **resources.json** | Resource data & targets | Yes | Your game data & optimization goals |

### buildings.json Structure
```json
[
  {
    "name": "Building Name",
    "maxWorkers": 4,
    "partialStaffingEfficiency": 0.9,
    "maintenanceCost": 0,
    "recipes": [
      {
        "name": "Recipe Name",
        "inputs": { "Resource": 2.0 },
        "outputs": { "Product": 3.0 },
        "productionRatePerWorkerPerYear": 150,
        "productionTimeInDays": 2
      }
    ]
  }
]
```

### resources.json Structure
```json
[
  {
    "name": "Resource Name",
    "stock": 500,
    "usedTotal": 5000,
    "usedLastYear": 1200,
    "producedTotal": 5200,
    "producedLastYear": 1300,
    "targetSurplus": 100,
    "isFinalProduct": true
  }
]
```

---

## 📊 Pre-Configured Content

### Buildings in buildings.json
1. **Tofu Workshop** - Soybean → Tofu
2. **Feast Hall** - Meat + Wheat + Vegetable → Feast
3. **Chocolate Workshop** - Cocoa + Sugar + Milk → Chocolate
4. **Tailor Shop** - Fabric + Dye → Custom Gown
5. **Tea House** - Tea Leaves → Black Tea
6. **Soap Workshop** - Oil + Lye → Soap
7. **Popcorn Stand** - Corn + Oil → Popcorn
8. **Distillery** - Wheat + Water → Whiskey
9. **Textile Mill** - Flax → Fabric
10. **Sugar Mill** - Sugar Cane → Sugar
11. **Farm (Soybean)** - → Soybean
12. **Farm (Wheat)** - → Wheat
13. **Farm (Tomato)** - → Tomato + Vegetable
14. **Farm (Sugar Cane)** - → Sugar Cane
15. **Farm (Flax)** - → Flax
16. **Farm (Corn)** - → Corn

### Resources in resources.json
1. **Tofu** (Final Product)
2. **Feast** (Final Product)
3. **Chocolate** (Final Product)
4. **Custom Gown** (Final Product)
5. **Black Tea** (Final Product)
6. **Soap** (Final Product)
7. **Popcorn** (Final Product)
8. **Whiskey** (Final Product)

---

## 🔧 Generated Files

These files are created when you run the application:

| File Pattern | Purpose |
|--------------|---------|
| `optimization_report_YYYYMMDD_HHMMSS.txt` | Detailed optimization reports |
| `bin/` | Compiled binaries (gitignored) |
| `obj/` | Build artifacts (gitignored) |

---

## 📖 Documentation Reading Order

### For New Users
1. **Overview.md** - Understand what this is
2. **INSTALLATION.md** - Get .NET SDK installed
3. **QUICKSTART.md** - Run your first optimization
4. **SAMPLE_REPORT.md** - Learn to read output
5. **CHECKLIST.md** - Follow implementation steps

### For Power Users
1. **README.md** - Complete feature reference
2. **DESIGN.md** - Understand the algorithms
3. **Data/buildings.json** - Customize production rates
4. **Services/*.cs** - Modify optimization logic

### For Developers
1. **DESIGN.md** - Architecture overview
2. **Models/*.cs** - Data structures
3. **Services/*.cs** - Business logic
4. **Program.cs** - Application flow

---

## 🎯 Key Files to Customize

### Must Edit for Your City
- ✏️ **Data/resources.json** - Update with YOUR game statistics

### May Want to Edit
- ✏️ **Data/buildings.json** - Adjust production rates for your game version
- ✏️ **Data/buildings.json** - Add new buildings from mods

### Usually Don't Edit
- ✅ **Models/*.cs** - Data structures
- ✅ **Services/*.cs** - Core logic
- ✅ **Program.cs** - Application entry

---

## 📂 Directory Tree

```
SettlementSurvivalOptimizer/
│
├── 📄 Overview.md                    ← Start here
├── 📄 README.md                      ← Complete docs
├── 📄 QUICKSTART.md                  ← Fast track
├── 📄 INSTALLATION.md                ← Setup guide
├── 📄 DESIGN.md                      ← Architecture
├── 📄 PROJECT_SUMMARY.md             ← What was built
├── 📄 SAMPLE_REPORT.md               ← Example output
├── 📄 CHECKLIST.md                   ← Implementation steps
├── 📄 FILE_INDEX.md                  ← This file
│
├── 📄 Program.cs                     ← Main app
├── 📄 SettlementSurvivalOptimizer.csproj
├── 📄 SettlementSurvivalOptimizer.sln
├── 📄 .gitignore
│
├── 📁 Models/
│   ├── Resource.cs
│   ├── Recipe.cs
│   ├── Building.cs
│   ├── ProductionChain.cs
│   ├── BuildingAllocation.cs
│   └── OptimizationResult.cs
│
├── 📁 Services/
│   ├── ProductionOptimizer.cs       ← Core algorithm
│   ├── DataLoader.cs                ← JSON I/O
│   └── ReportGenerator.cs           ← Report output
│
└── 📁 Data/
    ├── buildings.json               ← Edit to customize
    └── resources.json               ← Edit with YOUR data
```

---

## 🔍 Finding What You Need

### "How do I..."

**...install and set up?**
→ Read: `INSTALLATION.md`

**...run my first optimization?**
→ Read: `QUICKSTART.md`

**...understand the output?**
→ Read: `SAMPLE_REPORT.md`

**...update with my game data?**
→ Edit: `Data/resources.json`
→ Guide: `QUICKSTART.md` (Step 1)

**...add a new building?**
→ Edit: `Data/buildings.json`
→ Guide: `QUICKSTART.md` (Advanced section)

**...understand how it works?**
→ Read: `DESIGN.md`

**...modify the algorithm?**
→ Edit: `Services/ProductionOptimizer.cs`
→ Read: `DESIGN.md` first

**...change the report format?**
→ Edit: `Services/ReportGenerator.cs`

---

## 📊 File Statistics

- **Total Files**: 24
- **Documentation Files**: 8
- **Source Code Files**: 10 (C#)
- **Configuration Files**: 3
- **Project Files**: 3

---

## 🔄 File Change Frequency

### Frequently Modified
- `Data/resources.json` - Every optimization cycle

### Occasionally Modified
- `Data/buildings.json` - When adding buildings or adjusting rates

### Rarely Modified
- `Services/*.cs` - Only for feature enhancements
- `Models/*.cs` - Only for data model changes
- `Program.cs` - Only for UI changes

### Never Modified
- Documentation files (unless improving docs)
- Project configuration files

---

## 💾 Backup Recommendations

### Essential to Backup
- ✅ `Data/resources.json` - Your game data
- ✅ `Data/buildings.json` - Your customizations
- ✅ `optimization_report_*.txt` - Historical reports

### Nice to Backup
- ✅ Modified source files (if you customized)

### No Need to Backup
- ❌ `bin/` and `obj/` directories (regenerated)
- ❌ Documentation (in git repo)

---

## 🎯 Quick Reference Card

```
┌─────────────────────────────────────────────────────────────┐
│ QUICK FILE REFERENCE                                        │
├─────────────────────────────────────────────────────────────┤
│ Get Started          → Overview.md                          │
│ Install .NET         → INSTALLATION.md                      │
│ First Use            → QUICKSTART.md                        │
│ Update Your Data     → Data/resources.json                  │
│ Add Buildings        → Data/buildings.json                  │
│ Understand Output    → SAMPLE_REPORT.md                     │
│ Follow Checklist     → CHECKLIST.md                         │
│ Technical Details    → DESIGN.md                            │
│ All Features         → README.md                            │
│ Project Summary      → PROJECT_SUMMARY.md                   │
└─────────────────────────────────────────────────────────────┘
```

---

*This index is your map to the entire project. Bookmark it!* 📚✨
