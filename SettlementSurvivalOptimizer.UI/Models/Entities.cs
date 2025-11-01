using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SettlementSurvivalOptimizer.UI.Models;

public class BuildingEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public int MaxWorkers { get; set; }
    
    public double PartialStaffingEfficiency { get; set; } = 0.9;
    
    public double MaintenanceCost { get; set; }
    
    public List<RecipeEntity> Recipes { get; set; } = new();
}

public class RecipeEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public int BuildingId { get; set; }
    public BuildingEntity Building { get; set; } = null!;
    
    public double ProductionRatePerWorkerPerYear { get; set; }
    
    public double ProductionTimeInDays { get; set; }
    
    public List<RecipeInputEntity> Inputs { get; set; } = new();
    public List<RecipeOutputEntity> Outputs { get; set; } = new();
}

public class RecipeInputEntity
{
    [Key]
    public int Id { get; set; }
    
    public int RecipeId { get; set; }
    public RecipeEntity Recipe { get; set; } = null!;
    
    [Required]
    public string ResourceName { get; set; } = string.Empty;
    
    public double Quantity { get; set; }
}

public class RecipeOutputEntity
{
    [Key]
    public int Id { get; set; }
    
    public int RecipeId { get; set; }
    public RecipeEntity Recipe { get; set; } = null!;
    
    [Required]
    public string ResourceName { get; set; } = string.Empty;
    
    public double Quantity { get; set; }
}

public class ResourceEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public double Stock { get; set; }
    
    public double UsedTotal { get; set; }
    
    public double UsedLastYear { get; set; }
    
    public double ProducedTotal { get; set; }
    
    public double ProducedLastYear { get; set; }
    
    public double TargetSurplus { get; set; }
    
    public bool IsFinalProduct { get; set; }
}

public class AppSettingEntity
{
    [Key]
    public string Key { get; set; } = string.Empty;
    
    public string Value { get; set; } = string.Empty;
}
