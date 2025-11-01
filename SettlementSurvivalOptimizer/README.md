# Settlement Survival Production Optimizer

A .NET application designed to help maximize production efficiency and labor allocation in Settlement Survival by using backward-chaining analysis from target products through the entire production chain.

## Problem Solved

This optimizer addresses the common challenge of:
- **Overproduction**: Too many workers assigned leads to resource surplus and wasted labor
- **Underproduction**: Too few workers leads to shortages and economic instability
- **Chain Complexity**: Difficulty tracking intermediate resources consumed in production chains
- **Manual Balancing**: Time-consuming micromanagement of production buildings

## Key Features

### 🎯 Smart Backward Chaining
- Input your target final products (tofu, whiskey, etc.)
- System automatically calculates ALL intermediate resource requirements
- Works backward through entire production chain (e.g., Custom Gown → Fabric → Flax → Farm)

### 📊 Adaptive Optimization
- Uses historical data (last year's usage/production) to predict demand
- Accounts for current stock levels
- Recommends target surplus/deficit to maintain buffers
- Warns about low stock situations

### 👷 Intelligent Worker Allocation
- Calculates optimal worker count per building
- Considers partial staffing efficiency penalties
- Suggests building activation/deactivation strategies
- Respects population constraints

### 📈 Production Balance Analysis
- Shows annual production vs consumption for all resources
- Identifies potential bottlenecks in production chains
- Highlights surplus/deficit projections
- Provides efficiency ratings

## How It Works

### System Architecture

```
Target Products (Input)
    ↓
Demand Calculator (Backward Chaining)
    ↓
Resource Requirements (All Intermediate Resources)
    ↓
Production Allocator
    ↓
Building & Worker Allocation (Output)
```

### The Algorithm

1. **Demand Calculation**: 
   - Starts with final products and their consumption rates
   - Works backward through production recipes
   - Calculates total demand for each intermediate resource
   - Accounts for multi-input recipes (e.g., Chocolate needs Cocoa + Sugar + Milk)

2. **Building Allocation**:
   - Determines which buildings produce each required resource
   - Calculates workers needed based on production rates
   - Optimizes building count vs workers per building
   - Considers efficiency penalties for partial staffing

3. **Balance Projection**:
   - Sums total production across all buildings
   - Subtracts intermediate consumption
   - Subtracts final product consumption
   - Shows net surplus/deficit

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- Windows/Linux/macOS

### Installation

1. Navigate to the project directory:
```bash
cd SettlementSurvivalOptimizer
```

2. Build the project:
```bash
dotnet build
```

3. Run the optimizer:
```bash
dotnet run
```

## Configuration

### Buildings Data (`Data/buildings.json`)

Define your production buildings with:
- `name`: Building type name
- `maxWorkers`: Maximum worker capacity
- `partialStaffingEfficiency`: Efficiency penalty for partial staffing (0.0-1.0)
- `recipes`: List of production recipes

**Recipe Format:**
```json
{
  "name": "Tofu Production",
  "inputs": {
    "Soybean": 2.0
  },
  "outputs": {
    "Tofu": 3.0
  },
  "productionRatePerWorkerPerYear": 150,
  "productionTimeInDays": 2
}
```

### Resources Data (`Data/resources.json`)

Configure your target products with:
- `name`: Resource/product name
- `stock`: Current stock level
- `usedLastYear`: Annual consumption
- `producedLastYear`: Annual production
- `targetSurplus`: Desired surplus (+) or acceptable deficit (-)
- `isFinalProduct`: Mark as true for optimization targets

**Example:**
```json
{
  "name": "Tofu",
  "stock": 500,
  "usedLastYear": 1200,
  "producedLastYear": 1300,
  "targetSurplus": 100,
  "isFinalProduct": true
}
```

## Usage Examples

### Basic Workflow

1. **Input Population**: Enter your available population
2. **Configure Targets**: Edit `resources.json` with your target products and surplus goals
3. **Run Optimization**: Application calculates optimal allocations
4. **Review Report**: Check building allocations and resource balances
5. **Implement**: Adjust your settlement based on recommendations

### Sample Output

```
BUILDING ALLOCATIONS
─────────────────────────────────────────────────────────────────
Tofu Workshop
  Recipe: Tofu Production
  Buildings: 3
  Workers per Building: 3.0
  Total Workers: 9
  Efficiency: 75.0%
  Expected Production (annual):
    → Tofu: 1300 units/year
  Expected Consumption (annual):
    ← Soybean: 867 units/year
```

### Interactive Features

After running optimization, you can:
1. **Modify Resource Targets**: Adjust surplus/deficit goals
2. **Change Population**: Update available workers
3. **Re-run Optimization**: See new results instantly
4. **Export Data**: Save updated configurations

## Advanced Features

### Multi-Output Recipes
The system handles recipes with multiple outputs (e.g., Tomato farming produces both Tomatoes and Vegetables).

### Production Chain Depth
Automatically follows chains of any depth:
- Custom Gown ← Fabric ← Flax ← Farm

### Stock-Level Warnings
Alerts when stock falls below 25% of annual usage and adjusts production targets accordingly.

### Efficiency Optimization
Considers partial staffing penalties to maximize output per worker.

## Customization

### Adding New Buildings

Edit `Data/buildings.json` and add your building configuration:

```json
{
  "name": "Your Workshop",
  "maxWorkers": 5,
  "partialStaffingEfficiency": 0.9,
  "recipes": [
    {
      "name": "Your Recipe",
      "inputs": { "Input1": 2.0, "Input2": 1.0 },
      "outputs": { "Output": 3.0 },
      "productionRatePerWorkerPerYear": 150,
      "productionTimeInDays": 2
    }
  ]
}
```

### Adding New Products

Edit `Data/resources.json` and add your product:

```json
{
  "name": "New Product",
  "stock": 0,
  "usedLastYear": 500,
  "producedLastYear": 480,
  "targetSurplus": 50,
  "isFinalProduct": true
}
```

## Tips for Best Results

1. **Accurate Historical Data**: Use real "last year" numbers from your game
2. **Reasonable Buffers**: Set target surplus to ~10-20% of annual usage
3. **Monitor Stock**: Pay attention to low-stock warnings
4. **Iterate**: Re-run after implementing changes to fine-tune
5. **Population Constraints**: Prioritize critical products if population-limited

## Production Chain Examples

### Tofu Chain
- Tofu ← Soybean ← Farm (Soybean)

### Custom Gown Chain
- Custom Gown ← Fabric + Dye
  - Fabric ← Flax ← Farm (Flax)
  - Dye ← [External Source]

### Chocolate Chain
- Chocolate ← Cocoa + Sugar + Milk
  - Sugar ← Sugar Cane ← Farm (Sugar Cane)
  - Cocoa ← [External Source]
  - Milk ← [External Source]

### Whiskey Chain
- Whiskey ← Wheat + Water
  - Wheat ← Farm (Wheat)
  - Water ← [Natural Source]

## Future Enhancements

Potential features to add:
- [ ] Multi-producer optimization (choose best building for each resource)
- [ ] Seasonal variation support
- [ ] Trade route integration
- [ ] Technology/upgrade modeling
- [ ] Worker efficiency by skill level
- [ ] Building maintenance costs
- [ ] Storage capacity constraints
- [ ] GUI interface
- [ ] Save/load optimization scenarios
- [ ] Comparative analysis (before/after)

## Troubleshooting

**Issue**: "No production method found"
- **Solution**: Add the building that produces this resource to `buildings.json`

**Issue**: "Population constraint" warnings
- **Solution**: Increase population or reduce target surplus for some products

**Issue**: Large deficits shown
- **Solution**: May need external sources (trade, gathering) or additional buildings

## Contributing

To contribute or customize:
1. Fork the repository
2. Add your production chains to the JSON files
3. Modify optimization logic in `Services/ProductionOptimizer.cs`
4. Submit improvements!

## License

Free to use and modify for personal Settlement Survival gameplay optimization.

## Acknowledgments

- Inspired by the complexity of Settlement Survival's production chains
- Built to solve real gameplay optimization challenges
- Designed for minimal maintenance and maximum efficiency

---

**Happy Optimizing! May your settlements thrive! 🏘️**
