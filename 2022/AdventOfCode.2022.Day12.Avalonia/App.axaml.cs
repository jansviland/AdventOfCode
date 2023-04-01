using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Visuals.Media.Imaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdventOfCode._2022.Day12.Avalonia;

public partial class App : Application
{
    private static IHost Host { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<ISolutionService, SolutionService>();
        services.AddSingleton<MainWindow>();
    }

    public override void Initialize()
    {
        RenderOptions.BitmapInterpolationModeProperty.OverrideMetadata(typeof(Window), new StyledPropertyMetadata<BitmapInterpolationMode>(BitmapInterpolationMode.HighQuality));

        AvaloniaXamlLoader.Load(this);

        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) => { ConfigureServices(services); })
            .Build();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // use dependency injection to create the main window
            desktop.MainWindow = Host.Services.GetRequiredService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}