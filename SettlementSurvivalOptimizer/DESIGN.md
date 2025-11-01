# Settlement Survival Optimizer - System Design Document

## Executive Summary

This document describes the architecture and design decisions for a .NET application that optimizes production chains and worker allocation in the game Settlement Survival.

## Problem Statement

Players of Settlement Survival face challenges in balancing production:
1. **Overproduction**: Excess resources waste worker labor
2. **Underproduction**: Shortages disrupt the economy
3. **Complex Chains**: Multi-step production chains are hard to balance manually
4. **Constant Micromanagement**: Requires continuous adjustment

## Solution Architecture

### Core Design Philosophy

**Backward Chaining Optimization**: Instead of bottom-up calculation, we start with desired final products and work backward through the production chain to determine all intermediate resource requirements.

This approach ensures:
- All dependencies are accounted for
- No missing intermediate resources
- Optimal allocation from final demand
- Clear visibility of entire production chain

### System Components

```
┌─────────────────────────────────────────────────────────┐
│                    User Interface                       │
│  - Console-based interaction                           │
│  - Report generation                                   │
│  - Configuration management                            │
└────────────────┬────────────────────────────────────────┘
                 │
┌────────────────▼────────────────────────────────────────┐
│              Data Layer (JSON)                          │
│  - buildings.json (production definitions)             │
│  - resources.json (targets & historical data)          │
└────────────────┬────────────────────────────────────────┘
                 │
┌────────────────▼────────────────────────────────────────┐
│           ProductionOptimizer Service                   │
│  ┌──────────────────────────────────────────────────┐  │
│  │ 1. Demand Calculator                             │  │
│  │    - Backward chain traversal                    │  │
│  │    - Resource dependency resolution              │  │
│  │    - Stock-level analysis                        │  │
│  └──────────────────────────────────────────────────┘  │
│  ┌──────────────────────────────────────────────────┐  │
│  │ 2. Production Allocator                          │  │
│  │    - Worker requirement calculation              │  │
│  │    - Building count optimization                 │  │
│  │    - Efficiency modeling                         │  │
│  └──────────────────────────────────────────────────┘  │
│  ┌──────────────────────────────────────────────────┐  │
│  │ 3. Balance Analyzer                              │  │
│  │    - Production vs consumption                   │  │
│  │    - Surplus/deficit projection                  │  │
│  │    - Warning generation                          │  │
│  └──────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────┘
```

## Data Models

### Resource
Represents a game resource or product with:
- Current state (stock, usage, production)
- Historical data (last year's metrics)
- Optimization targets (desired surplus/deficit)
- Classification (final product vs intermediate)

### Recipe
Defines a production transformation:
- Inputs (resources consumed)
- Outputs (resources produced)
- Production rate (output per worker per time)
- Time requirements

### Building
Production facility definition:
- Max worker capacity
- Available recipes
- Efficiency characteristics
- Maintenance costs (future)

### ProductionChain
Graph structure representing:
- All buildings and recipes
- Resource producer/consumer mappings
- Dependency relationships

### BuildingAllocation
Optimization result for a building type:
- Building count needed
- Worker allocation
- Expected production/consumption
- Efficiency rating

### OptimizationResult
Complete solution:
- All building allocations
- Resource balances
- Warnings and issues
- Overall efficiency metrics

## Algorithm Details

### 1. Demand Calculation (Backward Chaining)

```
function CalculateResourceDemands(finalProducts):
    demands = {}
    queue = new Queue()
    
    # Initialize with final product requirements
    for each product in finalProducts:
        required = product.usedLastYear + product.targetSurplus
        
        # Adjust for low stock
        if product.stock < product.usedLastYear * 0.25:
            required += (product.usedLastYear * 0.25 - product.stock)
        
        demands[product.name] = required
        queue.enqueue(product.name)
    
    # Traverse production chain backward
    while queue not empty:
        resourceName = queue.dequeue()
        
        # Find production method
        building = FindProducer(resourceName)
        recipe = building.GetRecipe(resourceName)
        
        # Calculate batches needed
        batchesNeeded = demands[resourceName] / recipe.outputs[resourceName]
        
        # Add input demands
        for each input in recipe.inputs:
            inputDemand = input.quantity * batchesNeeded
            
            if input.name in demands:
                demands[input.name] += inputDemand
            else:
                demands[input.name] = inputDemand
                queue.enqueue(input.name)
    
    return demands
```

### 2. Production Allocation

```
function AllocateProductionCapacity(demands, population):
    allocations = []
    
    for each (resource, demand) in demands:
        building = FindProducer(resource)
        recipe = building.GetRecipe(resource)
        
        # Calculate workers needed
        workersNeeded = demand / recipe.productionRatePerWorker
        
        # Optimize building count
        buildingCount = ceiling(workersNeeded / building.maxWorkers)
        workersPerBuilding = workersNeeded / buildingCount
        
        # Calculate efficiency
        efficiency = CalculateEfficiency(
            workersPerBuilding, 
            building.maxWorkers, 
            building.partialStaffingPenalty
        )
        
        # Create allocation
        allocations.add(new Allocation(
            building, 
            buildingCount, 
            workersPerBuilding, 
            efficiency
        ))
    
    return allocations
```

### 3. Balance Analysis

```
function CalculateResourceBalance(allocations, finalProducts):
    balance = {}
    
    # Sum all production
    for each allocation in allocations:
        for each (resource, amount) in allocation.production:
            balance[resource] += amount
    
    # Subtract intermediate consumption
    for each allocation in allocations:
        for each (resource, amount) in allocation.consumption:
            balance[resource] -= amount
    
    # Subtract final product consumption
    for each product in finalProducts:
        balance[product.name] -= product.usedLastYear
    
    return balance
```

## Optimization Strategies

### Worker Allocation Strategy

**Goal**: Maximize production efficiency while minimizing worker count

**Approach**:
1. Calculate ideal worker count for target production
2. Distribute workers across buildings to minimize partial-staffing penalty
3. Prefer fully-staffed buildings over many partially-staffed ones
4. Consider efficiency curves (e.g., 50% staffing ≠ 50% output)

### Building Count Optimization

**Trade-off**: More buildings = better worker distribution = higher efficiency
But: More buildings = higher maintenance (future feature)

**Current Strategy**: 
- Use minimum buildings that keep average staffing ≥ 75%
- Adjust based on partial staffing efficiency factor

### Stock-Level Intelligence

**Low Stock Detection**:
- If stock < 25% of annual usage → WARNING
- Automatically boost production target to rebuild buffer
- Prevents cascading shortages

**Buffer Recommendations**:
- Critical products: 50% annual usage
- Standard products: 25% annual usage
- Abundant products: 10% annual usage

## Efficiency Modeling

### Partial Staffing Efficiency

Real observation: Partially staffed buildings are less efficient per worker.

**Model**: 
```
actualOutput = (workers / maxWorkers) * baseProduction * efficiencyFactor
```

Where `efficiencyFactor` ranges from 0.85 to 0.95 depending on building type.

### Overall Efficiency Score

```
efficiency = (Σ allocation.efficiency * allocation.workers) / totalWorkers
           * min(1.0, totalWorkers / availablePopulation)
```

This rewards:
- High per-building efficiency
- Good population utilization

## Future Enhancements

### Multi-Producer Optimization

**Current**: Uses first producer found for each resource
**Future**: Compare all producers and choose best (cost, efficiency, space)

### Seasonal Variations

**Current**: Uses annual averages
**Future**: Model seasonal demand variations (e.g., heating in winter)

### Trade Integration

**Current**: External resources marked as warnings
**Future**: Integrate trade routes as resource sources with costs

### Technology Unlocks

**Current**: Static production rates
**Future**: Model technology upgrades and efficiency improvements

### Storage Constraints

**Current**: Unlimited storage assumed
**Future**: Model storage capacity and recommend expansions

### Worker Skills

**Current**: All workers identical
**Future**: Model skilled vs unskilled workers

## Performance Considerations

### Time Complexity

- **Demand Calculation**: O(R * D) where R = resources, D = chain depth
- **Allocation**: O(B) where B = building types
- **Balance**: O(A) where A = allocations

Typical run: < 100ms for 20 buildings, 50 resources

### Space Complexity

- **Production Chain Graph**: O(B + R + E) where E = recipe edges
- **Optimization Result**: O(B + R)

Typical memory: < 5MB

## Testing Strategy

### Unit Tests (Future)
- Demand calculation correctness
- Allocation algorithm accuracy
- Efficiency calculations
- Edge cases (zero demand, no producers, etc.)

### Integration Tests (Future)
- Full optimization pipeline
- Data loading/saving
- Report generation

### Validation Tests
- Compare with manual calculations
- Verify chain traversal completeness
- Check balance accuracy

## Configuration Management

### buildings.json Design

**Flexibility**: Easy to add new buildings/recipes
**Validation**: Should add schema validation
**Updates**: Can be updated without code changes

### resources.json Design

**Purpose**: Both configuration and state
**Updates**: Modified by user and application
**History**: Consider keeping historical snapshots

## User Experience

### Report Design

**Priorities**:
1. Executive summary (quick overview)
2. Warnings (immediate attention needed)
3. Building allocations (action items)
4. Resource balance (validation)

**Format**: Console-based with clear sections and visual separators

### Interactive Mode

**Features**:
- Modify targets on-the-fly
- Re-run optimization
- Compare scenarios (future)
- Save/load configurations

## Extensibility

### Plugin Architecture (Future)

Allow custom:
- Optimization strategies
- Efficiency models
- Report formats
- Data sources

### API Design (Future)

RESTful API for:
- Web interface
- Mobile app
- Community tools

## Conclusion

This system provides a sophisticated yet practical solution to Settlement Survival's production optimization challenge. The backward-chaining approach ensures complete coverage of production chains, while the efficiency modeling provides realistic recommendations.

Key strengths:
- ✓ Handles complex multi-tier production chains
- ✓ Accounts for partial staffing penalties
- ✓ Provides actionable building/worker recommendations
- ✓ Easily configurable via JSON
- ✓ Extensible architecture

The system minimizes micromanagement by providing clear, data-driven recommendations that players can implement once and adjust only when goals change.
