using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SettlementSurvivalOptimizer.UI.ViewModels;
using SettlementSurvivalOptimizer.UI.Views;

namespace SettlementSurvivalOptimizer.UI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            try
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }
            catch (Exception ex)
            {
                // Show error in a message box
                var errorWindow = new Window
                {
                    Title = "Error",
                    Width = 600,
                    Height = 400,
                    Content = new TextBlock
                    {
                        Text = $"Failed to initialize application:\n\n{ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                        TextWrapping = Avalonia.Media.TextWrapping.Wrap,
                        Margin = new Avalonia.Thickness(10)
                    }
                };
                desktop.MainWindow = errorWindow;
            }
        }

        base.OnFrameworkInitializationCompleted();
    }
}
