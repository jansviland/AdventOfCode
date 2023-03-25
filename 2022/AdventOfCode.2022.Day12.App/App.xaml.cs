using System.Windows;
using System.Windows.Media;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdventOfCode._2022.Day12.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static IHost Host { get; set; }

        public App()
        {
            Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) => { ConfigureServices(services); })
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISolutionService, SolutionService>();
            services.AddSingleton<MainWindow>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            // set quality of rendering for all windows to high quality
            RenderOptions.EdgeModeProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(EdgeMode.Aliased));
            RenderOptions.ClearTypeHintProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(ClearTypeHint.Enabled));
            RenderOptions.BitmapScalingModeProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(BitmapScalingMode.HighQuality));

            await Host!.StartAsync();

            var mainWindow = Host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await Host!.StopAsync();
            base.OnExit(e);
        }
    }
}