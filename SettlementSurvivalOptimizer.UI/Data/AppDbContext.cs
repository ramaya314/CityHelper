using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using SettlementSurvivalOptimizer.UI.Models;

namespace SettlementSurvivalOptimizer.UI.Data;

public class AppDbContext : DbContext
{
    public DbSet<BuildingEntity> Buildings { get; set; }
    public DbSet<RecipeEntity> Recipes { get; set; }
    public DbSet<RecipeInputEntity> RecipeInputs { get; set; }
    public DbSet<RecipeOutputEntity> RecipeOutputs { get; set; }
    public DbSet<ResourceEntity> Resources { get; set; }
    public DbSet<AppSettingEntity> Settings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "SettlementSurvivalOptimizer",
            "optimizer.db");
        
        var directory = Path.GetDirectoryName(dbPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Building configuration
        modelBuilder.Entity<BuildingEntity>()
            .HasKey(b => b.Id);
        
        modelBuilder.Entity<BuildingEntity>()
            .HasMany(b => b.Recipes)
            .WithOne(r => r.Building)
            .HasForeignKey(r => r.BuildingId)
            .OnDelete(DeleteBehavior.Cascade);

        // Recipe configuration
        modelBuilder.Entity<RecipeEntity>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<RecipeEntity>()
            .HasMany(r => r.Inputs)
            .WithOne(i => i.Recipe)
            .HasForeignKey(i => i.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RecipeEntity>()
            .HasMany(r => r.Outputs)
            .WithOne(o => o.Recipe)
            .HasForeignKey(o => o.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Recipe Input/Output configuration
        modelBuilder.Entity<RecipeInputEntity>()
            .HasKey(i => i.Id);

        modelBuilder.Entity<RecipeOutputEntity>()
            .HasKey(o => o.Id);

        // Resource configuration
        modelBuilder.Entity<ResourceEntity>()
            .HasKey(r => r.Id);

        // Settings configuration
        modelBuilder.Entity<AppSettingEntity>()
            .HasKey(s => s.Key);
    }
}
