using SettlementSurvivalOptimizer.Models;
using SettlementSurvivalOptimizer.Services;

namespace SettlementSurvivalOptimizer;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("═══════════════════════════════════════════════════════════════════");
        Console.WriteLine("     SETTLEMENT SURVIVAL - PRODUCTION OPTIMIZER");
        Console.WriteLine("═══════════════════════════════════════════════════════════════════");
        Console.WriteLine();

        try
        {
            // Initialize services
            var dataLoader = new DataLoader();
            var productionChain = dataLoader.LoadProductionChain();
            var resources = dataLoader.LoadResources();
            
            Console.WriteLine($"Loaded {productionChain.Buildings.Count} building types");
            Console.WriteLine($"Loaded {resources.Count} resources");
            Console.WriteLine();

            // Get population
            Console.Write("Enter your current population: ");
            if (!int.TryParse(Console.ReadLine(), out int population) || population <= 0)
            {
                Console.WriteLine("Invalid population. Using default: 200");
                population = 200;
            }
            Console.WriteLine();

            // Show final products
            var finalProducts = resources.Where(r => r.IsFinalProduct).ToList();
            Console.WriteLine("Final Products Configured:");
            Console.WriteLine("─────────────────────────────────────────────────────────────────");
            foreach (var product in finalProducts)
            {
                Console.WriteLine($"  {product.Name}");
                Console.WriteLine($"    Used (last year): {product.UsedLastYear:F0}");
                Console.WriteLine($"    Produced (last year): {product.ProducedLastYear:F0}");
                Console.WriteLine($"    Target Surplus: {product.TargetSurplus:F0}");
                Console.WriteLine($"    Current Stock: {product.Stock:F0}");
            }
            Console.WriteLine();

            // Run optimization
            Console.WriteLine("Running optimization...");
            Console.WriteLine();
            
            var optimizer = new ProductionOptimizer(productionChain);
            var result = optimizer.Optimize(finalProducts, population);

            // Generate and display report
            var reportGenerator = new ReportGenerator();
            var report = reportGenerator.GenerateDetailedReport(result, resources, population);
            
            Console.WriteLine(report);

            // Save report to file
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var reportPath = $"optimization_report_{timestamp}.txt";
            File.WriteAllText(reportPath, report);
            Console.WriteLine($"\nReport saved to: {reportPath}");
            
            // Interactive menu
            ShowInteractiveMenu(dataLoader, productionChain, resources, population, optimizer, reportGenerator);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n❌ Error: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    static void ShowInteractiveMenu(DataLoader dataLoader, ProductionChain productionChain, 
        List<Resource> resources, int population, ProductionOptimizer optimizer, ReportGenerator reportGenerator)
    {
        while (true)
        {
            Console.WriteLine("\n─────────────────────────────────────────────────────────────────");
            Console.WriteLine("OPTIONS:");
            Console.WriteLine("  1. Modify resource targets");
            Console.WriteLine("  2. Change population");
            Console.WriteLine("  3. Re-run optimization");
            Console.WriteLine("  4. Export data");
            Console.WriteLine("  5. Exit");
            Console.Write("\nSelect option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ModifyResourceTargets(resources, dataLoader);
                    break;
                case "2":
                    Console.Write("Enter new population: ");
                    if (int.TryParse(Console.ReadLine(), out int newPop))
                        population = newPop;
                    break;
                case "3":
                    var finalProducts = resources.Where(r => r.IsFinalProduct).ToList();
                    var result = optimizer.Optimize(finalProducts, population);
                    var report = reportGenerator.GenerateDetailedReport(result, resources, population);
                    Console.WriteLine(report);
                    break;
                case "4":
                    dataLoader.SaveResources(resources);
                    Console.WriteLine("✓ Resources saved to Data/resources.json");
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }

    static void ModifyResourceTargets(List<Resource> resources, DataLoader dataLoader)
    {
        var finalProducts = resources.Where(r => r.IsFinalProduct).ToList();
        
        Console.WriteLine("\nFinal Products:");
        for (int i = 0; i < finalProducts.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {finalProducts[i].Name} (Target Surplus: {finalProducts[i].TargetSurplus})");
        }

        Console.Write("\nSelect product to modify (number) or 0 to cancel: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= finalProducts.Count)
        {
            var product = finalProducts[choice - 1];
            
            Console.Write($"Current target surplus for {product.Name}: {product.TargetSurplus}\n");
            Console.Write("Enter new target surplus: ");
            
            if (double.TryParse(Console.ReadLine(), out double newSurplus))
            {
                product.TargetSurplus = newSurplus;
                Console.WriteLine($"✓ Updated {product.Name} target surplus to {newSurplus}");
            }
        }
    }
}
