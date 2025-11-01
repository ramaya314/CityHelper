# Quick Start Guide

## Installation & First Run

1. **Open the project folder in terminal:**
   ```powershell
   cd "H:\Projects\CityHelper\SettlementSurvivalOptimizer"
   ```

2. **Restore dependencies and build:**
   ```powershell
   dotnet restore
   dotnet build
   ```

3. **Run the application:**
   ```powershell
   dotnet run
   ```

## First Time Setup

When you first run the application:

1. Enter your **current population** (e.g., 200)
2. Review the pre-configured final products
3. The optimizer will calculate optimal allocations
4. Review the generated report

## Customizing for Your City

### Step 1: Update Your Resources

Edit `Data/resources.json` with YOUR actual game data:

```json
{
  "name": "Tofu",
  "stock": 500,              ← Your current stock
  "usedLastYear": 1200,      ← Check in-game stats
  "producedLastYear": 1300,  ← Check in-game stats
  "targetSurplus": 100,      ← How much buffer you want
  "isFinalProduct": true     ← Keep true for products you want to optimize
}
```

**Where to find this data in Settlement Survival:**
- Open your Storage/Warehouse menu
- Look at each resource's statistics panel
- Note: "Last Year" = last full in-game year

### Step 2: Adjust Target Surplus

For each product, set `targetSurplus` based on your strategy:

- **Critical Products** (food, medicine): Set to 50% of annual usage
  - Example: If used = 1000/year, set surplus = 500
  
- **Standard Products** (clothing, tools): Set to 20% of annual usage
  - Example: If used = 500/year, set surplus = 100
  
- **Luxury Products** (whiskey, chocolate): Set to 10% of annual usage
  - Example: If used = 300/year, set surplus = 30

### Step 3: Add/Remove Products

**To track a new product:**
1. Add entry to `Data/resources.json`
2. Make sure the building that produces it exists in `Data/buildings.json`
3. Set `isFinalProduct: true`

**To stop tracking a product:**
1. Set `isFinalProduct: false` in `Data/resources.json`

## Understanding the Report

### Building Allocations Section

```
Tofu Workshop
  Buildings: 3              ← Build 3 Tofu Workshops
  Workers per Building: 3.0 ← Assign 3 workers to each
  Total Workers: 9          ← Total: 9 workers needed
  Efficiency: 75.0%         ← Operating at 75% efficiency
```

**Action**: Build 3 Tofu Workshops and assign 3 workers to each.

### Resource Balance Section

```
★ Tofu          SURPLUS      +100 units/year
  Soybean       DEFICIT       -50 units/year
```

- **SURPLUS**: You'll produce more than you use (good!)
- **DEFICIT**: Need external source (trade, gathering, or increase production)
- **★** = Final product you're optimizing for

### Warnings

Pay attention to warnings like:
- `Low stock warning`: Increase production ASAP
- `Population constraint`: Need more citizens or reduce targets

## Common Workflows

### Scenario 1: New Production Goal

**Goal**: Start producing Custom Gowns

1. Add Custom Gown to `resources.json`:
   ```json
   {
     "name": "Custom Gown",
     "stock": 0,
     "usedLastYear": 100,
     "producedLastYear": 0,
     "targetSurplus": 20,
     "isFinalProduct": true
   }
   ```

2. Run optimizer: `dotnet run`

3. Review allocations - you'll see:
   - Tailor Shop allocation
   - Fabric requirement (intermediate)
   - Flax farm requirement (raw material)

4. Implement in game

### Scenario 2: Adjust Existing Production

**Problem**: Too much Tofu, not enough Whiskey

1. Edit `resources.json`:
   ```json
   { "name": "Tofu", "targetSurplus": 50 }      ← Reduce from 100
   { "name": "Whiskey", "targetSurplus": 100 }  ← Increase from 50
   ```

2. Run optimizer

3. Follow new allocations

### Scenario 3: Population Increased

**Situation**: Population grew from 200 to 300

1. Run application
2. When prompted, enter `300`
3. Optimizer will use additional workers
4. Review how to allocate the 100 new workers

## Interactive Mode

After viewing the initial report, use the menu:

```
1. Modify resource targets  ← Adjust surplus goals
2. Change population        ← Update worker count
3. Re-run optimization      ← Calculate new plan
4. Export data              ← Save changes
5. Exit
```

### Typical Session

1. Run optimization with current data
2. Review report
3. Select option `1` to adjust a product's target
4. Select option `3` to re-run with new targets
5. Select option `4` to save changes
6. Implement in game

## Tips for Success

### 🎯 Start Small
- Begin with 2-3 final products
- Get comfortable with the system
- Gradually add more products

### 📊 Use Real Data
- Don't guess at "last year" numbers
- Wait for a full game year to get accurate stats
- Update data every few game years

### ⚖️ Set Realistic Buffers
- Start with 20% surplus for everything
- Adjust based on volatility
- Products you use constantly need bigger buffers

### 👷 Population Management
- Reserve 20-30% for non-production (builders, etc.)
- If optimizer needs 200, have 250 total population
- Prioritize critical products if constrained

### 🔄 Iterate
- Implement recommendations
- Play for a game year
- Update data with actual results
- Re-optimize

### 📝 Keep Notes
- Track which products are seasonal
- Note products that often shortage
- Document external sources (trade routes)

## Troubleshooting

### "No production method found for X"

**Cause**: Resource needed but building not in `buildings.json`

**Solution**: 
- If you produce it: Add the building to `buildings.json`
- If external (trade/gathering): Ignore warning, it's expected

### "Population constraint" warnings

**Cause**: Not enough workers for optimal production

**Solutions**:
1. Increase population (build houses, attract migrants)
2. Reduce target surplus for some products
3. Prioritize critical products

### Huge deficits shown

**Causes**:
- External resources (imported goods)
- Gathering (wood, stone, water)
- Hunting/fishing

**Solution**: These are expected if you don't produce them directly

### Numbers don't match game exactly

**Causes**:
- Seasonal variations (game has seasons, optimizer uses annual average)
- Technology bonuses not modeled
- Worker skill levels not modeled

**Solution**: Use optimizer as a guideline, adjust based on in-game results

## Advanced: Adding Custom Buildings

If you have mods or the game updates with new buildings:

1. Edit `Data/buildings.json`
2. Add new building following this template:

```json
{
  "name": "Your Building Name",
  "maxWorkers": 5,
  "partialStaffingEfficiency": 0.9,
  "maintenanceCost": 0,
  "recipes": [
    {
      "name": "Recipe Name",
      "inputs": {
        "Input Resource 1": 2.0,
        "Input Resource 2": 1.0
      },
      "outputs": {
        "Output Resource": 3.0
      },
      "productionRatePerWorkerPerYear": 150,
      "productionTimeInDays": 2
    }
  ]
}
```

**How to calculate `productionRatePerWorkerPerYear`:**

1. Note production time in days
2. Note output per batch
3. Calculate: `(365 / productionTimeInDays) * outputPerBatch`
4. Divide by typical worker count

Example: 
- 2 days per batch
- 3 units per batch  
- 4 workers typical
- Rate = (365 / 2) * 3 / 4 = 137 units/worker/year

## Next Steps

Once comfortable:

1. **Expand Coverage**: Add more products to optimize
2. **Refine Targets**: Adjust surplus based on experience
3. **Track Changes**: Keep old reports to compare
4. **Share Configs**: Share your `buildings.json` with other players

## Getting Help

If stuck:
1. Check the `README.md` for detailed documentation
2. Review `DESIGN.md` for how the system works
3. Examine the sample data in `Data/` folder
4. Check warning messages in reports

---

**Remember**: This tool provides recommendations based on data. You're still the mayor - adjust based on your city's unique needs! 🏘️
