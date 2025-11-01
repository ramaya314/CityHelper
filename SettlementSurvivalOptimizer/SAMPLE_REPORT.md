# Sample Optimization Report

This is an example of what the optimizer output looks like.

═══════════════════════════════════════════════════════════════════
     SETTLEMENT SURVIVAL - PRODUCTION OPTIMIZATION REPORT
═══════════════════════════════════════════════════════════════════

EXECUTIVE SUMMARY
─────────────────────────────────────────────────────────────────
Total Workers Required: 147 / 200 available
Population Utilization: 73.5%
Overall Efficiency: 85.2%
Number of Building Types: 13
Total Buildings Needed: 28

⚠ WARNINGS
─────────────────────────────────────────────────────────────────
  • Low stock warning: Black Tea has only 600 units (less than 25% of annual usage). Adding 0 to production target.

BUILDING ALLOCATIONS
─────────────────────────────────────────────────────────────────

Popcorn Stand
  Recipe: Popcorn Production
  Buildings: 3
  Workers per Building: 5.0
  Total Workers: 15
  Efficiency: 95.0%
  Expected Production (annual):
    → Popcorn: 2300 units/year
  Expected Consumption (annual):
    ← Corn: 920 units/year
    ← Oil: 230 units/year

Black Tea House
  Recipe: Black Tea Production
  Buildings: 3
  Workers per Building: 3.0
  Total Workers: 9
  Efficiency: 90.0%
  Expected Production (annual):
    → Black Tea: 1700 units/year
  Expected Consumption (annual):
    ← Tea Leaves: 1133 units/year

Tofu Workshop
  Recipe: Tofu Production
  Buildings: 3
  Workers per Building: 3.0
  Total Workers: 9
  Efficiency: 75.0%
  Expected Production (annual):
    → Tofu: 1350 units/year
  Expected Consumption (annual):
    ← Soybean: 900 units/year

Soap Workshop
  Recipe: Soap Production
  Buildings: 2
  Workers per Building: 3.4
  Total Workers: 7
  Efficiency: 85.0%
  Expected Production (annual):
    → Soap: 1120 units/year
  Expected Consumption (annual):
    ← Oil: 560 units/year
    ← Lye: 280 units/year

Chocolate Workshop
  Recipe: Chocolate Production
  Buildings: 2
  Workers per Building: 3.0
  Total Workers: 6
  Efficiency: 75.0%
  Expected Production (annual):
    → Chocolate: 900 units/year
  Expected Consumption (annual):
    ← Cocoa: 600 units/year
    ← Sugar: 300 units/year
    ← Milk: 300 units/year

Whiskey Distillery
  Recipe: Whiskey Production
  Buildings: 2
  Workers per Building: 2.9
  Total Workers: 6
  Efficiency: 58.0%
  Expected Production (annual):
    → Whiskey: 650 units/year
  Expected Consumption (annual):
    ← Wheat: 975 units/year
    ← Water: 650 units/year

Feast Hall
  Recipe: Feast Production
  Buildings: 2
  Workers per Building: 2.8
  Total Workers: 6
  Efficiency: 46.7%
  Expected Production (annual):
    → Feast: 550 units/year
  Expected Consumption (annual):
    ← Meat: 825 units/year
    ← Wheat: 550 units/year
    ← Vegetable: 550 units/year

Tailor Shop
  Recipe: Custom Gown Production
  Buildings: 2
  Workers per Building: 2.3
  Total Workers: 5
  Efficiency: 46.0%
  Expected Production (annual):
    → Custom Gown: 450 units/year
  Expected Consumption (annual):
    ← Fabric: 1800 units/year
    ← Dye: 450 units/year

Sugar Mill
  Recipe: Sugar Production
  Buildings: 1
  Workers per Building: 3.1
  Total Workers: 3
  Efficiency: 62.0%
  Expected Production (annual):
    → Sugar: 403 units/year
  Expected Consumption (annual):
    ← Sugar Cane: 537 units/year

Textile Mill
  Recipe: Fabric Production
  Buildings: 1
  Workers per Building: 4.5
  Total Workers: 5
  Efficiency: 75.0%
  Expected Production (annual):
    → Fabric: 1890 units/year
  Expected Consumption (annual):
    ← Flax: 1418 units/year

Farm (Corn)
  Recipe: Corn Farming
  Buildings: 2
  Workers per Building: 4.3
  Total Workers: 9
  Efficiency: 53.8%
  Expected Production (annual):
    → Corn: 1052 units/year

Farm (Soybean)
  Recipe: Soybean Farming
  Buildings: 1
  Workers per Building: 3.6
  Total Workers: 4
  Efficiency: 45.0%
  Expected Production (annual):
    → Soybean: 900 units/year

Farm (Sugar Cane)
  Recipe: Sugar Cane Farming
  Buildings: 1
  Workers per Building: 2.2
  Total Workers: 2
  Efficiency: 27.5%
  Expected Production (annual):
    → Sugar Cane: 528 units/year

Farm (Flax)
  Recipe: Flax Farming
  Buildings: 1
  Workers per Building: 6.2
  Total Workers: 6
  Efficiency: 88.6%
  Expected Production (annual):
    → Flax: 1426 units/year

RESOURCE BALANCE (Annual Projection)
─────────────────────────────────────────────────────────────────
Resource                      Production    Consumption    Balance
─────────────────────────────────────────────────────────────────
★ Popcorn                       SURPLUS         +300 units/year
★ Black Tea                     SURPLUS         +200 units/year
★ Tofu                          SURPLUS         +150 units/year
★ Soap                          SURPLUS         +120 units/year
★ Chocolate                     SURPLUS         +100 units/year
★ Custom Gown                   SURPLUS          +50 units/year
★ Whiskey                       SURPLUS          +50 units/year
★ Feast                         SURPLUS          +50 units/year
  Fabric                        SURPLUS          +90 units/year
  Sugar                         SURPLUS         +103 units/year
  Corn                          SURPLUS         +132 units/year
  Flax                          SURPLUS           +8 units/year
  Sugar Cane                    DEFICIT          -9 units/year
  Soybean                       BALANCED          +0 units/year
  Tea Leaves                    DEFICIT      -1,133 units/year
  Oil                           DEFICIT        -790 units/year
  Lye                           DEFICIT        -280 units/year
  Cocoa                         DEFICIT        -600 units/year
  Milk                          DEFICIT        -300 units/year
  Meat                          DEFICIT        -825 units/year
  Wheat                         DEFICIT      -1,525 units/year
  Water                         DEFICIT        -650 units/year
  Vegetable                     DEFICIT        -550 units/year
  Dye                           DEFICIT        -450 units/year

★ = Final Product Target

ACTION ITEMS
─────────────────────────────────────────────────────────────────
  → Build/Activate 2 Farm (Corn)(s) with 9 total workers
  → Build/Activate 1 Farm (Flax)(s) with 6 total workers
  → Build/Activate 1 Farm (Soybean)(s) with 4 total workers
  → Build/Activate 1 Farm (Sugar Cane)(s) with 2 total workers
  → Build/Activate 2 Feast Hall(s) with 6 total workers
  → Build/Activate 3 Black Tea House(s) with 9 total workers
  → Build/Activate 2 Chocolate Workshop(s) with 6 total workers
  → Build/Activate 3 Popcorn Stand(s) with 15 total workers
  → Build/Activate 2 Soap Workshop(s) with 7 total workers
  → Build/Activate 1 Sugar Mill(s) with 3 total workers
  → Build/Activate 2 Tailor Shop(s) with 5 total workers
  → Build/Activate 1 Textile Mill(s) with 5 total workers
  → Build/Activate 3 Tofu Workshop(s) with 9 total workers
  → Build/Activate 2 Whiskey Distillery(s) with 6 total workers

═══════════════════════════════════════════════════════════════════

## How to Read This Report

### Executive Summary
- Shows you used 147 of 200 available workers (73.5%)
- Overall efficiency of 85.2% means most buildings are well-staffed
- You need 28 total buildings across 13 different types

### Building Allocations
Each section shows:
- **Buildings**: How many of this type to build
- **Workers per Building**: How many workers to assign to each
- **Total Workers**: Total workers needed for this building type
- **Efficiency**: How efficiently this building operates
- **Expected Production**: What you'll produce annually
- **Expected Consumption**: What raw materials you'll use

### Resource Balance
- **SURPLUS**: You produce more than you consume (good!)
- **BALANCED**: Production matches consumption
- **DEFICIT**: Need external source (trade, gathering, hunting)
- **★ = Final Product**: These are your target products

Resources with deficits that aren't final products need to be:
- Gathered (Wood, Stone, Water)
- Hunted/Fished (Meat, Fish)
- Traded (Cocoa, Tea Leaves, Dye)
- Produced by buildings not yet configured

### Action Items
Clear list of what to build and how to staff each building.

## Implementation Strategy

1. **Phase 1 - Raw Materials** (Days 1-10):
   - Build all farms first
   - Assign workers
   - Let them accumulate resources

2. **Phase 2 - Intermediate Production** (Days 10-20):
   - Build Sugar Mill, Textile Mill
   - Start processing raw materials

3. **Phase 3 - Final Products** (Days 20-30):
   - Build all workshop/production buildings
   - Assign workers according to report

4. **Phase 4 - Monitor & Adjust** (Ongoing):
   - Watch stock levels
   - Verify production matches projections
   - Re-run optimizer if needed

## Expected Results

After implementing:
- All 8 final products meet target surplus
- 53 workers remain for other tasks (builders, services, etc.)
- Efficient use of population
- Stable production without constant intervention
- Clear visibility of external resource needs

## Notes

**Deficits are OK if:**
- They're gathered resources (Water, Stone)
- They're hunted (Meat, Fish)
- They're traded (Cocoa, Tea Leaves)
- You have external sources configured

**Re-optimize when:**
- Population changes significantly
- You want to change target products
- Consumption patterns change
- New buildings become available
- After major game updates

═══════════════════════════════════════════════════════════════════
