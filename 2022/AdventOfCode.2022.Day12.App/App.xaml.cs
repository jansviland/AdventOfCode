using System.Windows;
using System.Windows.Media;

namespace AdventOfCode._2022.Day12.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // set quality of rendering for all windows to high quality
            RenderOptions.EdgeModeProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(EdgeMode.Aliased));
            RenderOptions.ClearTypeHintProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(ClearTypeHint.Enabled));
            RenderOptions.BitmapScalingModeProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(BitmapScalingMode.HighQuality));

            base.OnStartup(e);
        }
    }
}