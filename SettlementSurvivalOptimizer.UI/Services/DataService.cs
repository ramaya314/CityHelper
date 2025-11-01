using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SettlementSurvivalOptimizer.UI.Data;
using SettlementSurvivalOptimizer.UI.Models;

namespace SettlementSurvivalOptimizer.UI.Services;

public class DataService
{
    private readonly AppDbContext _context;

    public DataService()
    {
        _context = new AppDbContext();
        _context.Database.EnsureCreated();
    }

    // Buildings
    public async Task<List<BuildingEntity>> GetAllBuildingsAsync()
    {
        // Detach all tracked entities to ensure we get fresh data
        _context.ChangeTracker.Clear();
        return await _context.Buildings
            .Include(b => b.Recipes)
                .ThenInclude(r => r.Inputs)
            .Include(b => b.Recipes)
                .ThenInclude(r => r.Outputs)
            .ToListAsync();
    }

    public async Task<BuildingEntity?> GetBuildingByIdAsync(int id)
    {
        return await _context.Buildings
            .Include(b => b.Recipes)
                .ThenInclude(r => r.Inputs)
            .Include(b => b.Recipes)
                .ThenInclude(r => r.Outputs)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task SaveBuildingAsync(BuildingEntity building)
    {
        if (building.Id == 0)
            _context.Buildings.Add(building);
        else
            _context.Buildings.Update(building);
        
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBuildingAsync(int id)
    {
        var building = await _context.Buildings.FindAsync(id);
        if (building != null)
        {
            _context.Buildings.Remove(building);
            await _context.SaveChangesAsync();
        }
    }

    // Resources
    public async Task<List<ResourceEntity>> GetAllResourcesAsync()
    {
        // Detach all tracked entities to ensure we get fresh data
        _context.ChangeTracker.Clear();
        return await _context.Resources.ToListAsync();
    }

    public async Task<ResourceEntity?> GetResourceByIdAsync(int id)
    {
        return await _context.Resources.FindAsync(id);
    }

    public async Task SaveResourceAsync(ResourceEntity resource)
    {
        if (resource.Id == 0)
            _context.Resources.Add(resource);
        else
            _context.Resources.Update(resource);
        
        await _context.SaveChangesAsync();
    }

    public async Task DeleteResourceAsync(int id)
    {
        var resource = await _context.Resources.FindAsync(id);
        if (resource != null)
        {
            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
        }
    }

    // Settings
    public async Task<string?> GetSettingAsync(string key)
    {
        var setting = await _context.Settings.FindAsync(key);
        return setting?.Value;
    }

    public async Task SaveSettingAsync(string key, string value)
    {
        var setting = await _context.Settings.FindAsync(key);
        if (setting == null)
        {
            setting = new AppSettingEntity { Key = key, Value = value };
            _context.Settings.Add(setting);
        }
        else
        {
            setting.Value = value;
            _context.Settings.Update(setting);
        }
        await _context.SaveChangesAsync();
    }

    // Initialize with sample data
    public async Task SeedSampleDataAsync()
    {
        if (await _context.Buildings.AnyAsync())
            return; // Already seeded

        var buildings = new List<BuildingEntity>
        {
            new BuildingEntity
            {
                Name = "Tofu Workshop",
                MaxWorkers = 4,
                PartialStaffingEfficiency = 0.9,
                Recipes = new List<RecipeEntity>
                {
                    new RecipeEntity
                    {
                        Name = "Tofu Production",
                        ProductionRatePerWorkerPerYear = 150,
                        ProductionTimeInDays = 2,
                        Inputs = new List<RecipeInputEntity>
                        {
                            new RecipeInputEntity { ResourceName = "Soybean", Quantity = 2.0 }
                        },
                        Outputs = new List<RecipeOutputEntity>
                        {
                            new RecipeOutputEntity { ResourceName = "Tofu", Quantity = 3.0 }
                        }
                    }
                }
            },
            new BuildingEntity
            {
                Name = "Farm (Soybean)",
                MaxWorkers = 8,
                PartialStaffingEfficiency = 0.95,
                Recipes = new List<RecipeEntity>
                {
                    new RecipeEntity
                    {
                        Name = "Soybean Farming",
                        ProductionRatePerWorkerPerYear = 250,
                        ProductionTimeInDays = 60,
                        Outputs = new List<RecipeOutputEntity>
                        {
                            new RecipeOutputEntity { ResourceName = "Soybean", Quantity = 10.0 }
                        }
                    }
                }
            }
        };

        _context.Buildings.AddRange(buildings);

        var resources = new List<ResourceEntity>
        {
            new ResourceEntity
            {
                Name = "Tofu",
                Stock = 500,
                UsedLastYear = 1200,
                ProducedLastYear = 1300,
                TargetSurplus = 100,
                IsFinalProduct = true
            }
        };

        _context.Resources.AddRange(resources);
        await _context.SaveChangesAsync();
    }
}
