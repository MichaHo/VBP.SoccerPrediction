using SoccerPrediction.ViewModel;
using SoccerPrediction.ViewModel.Services;
using System.Windows;

namespace SoccerPrediction.Wpf.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ServiceInjector.InjectServices();
            var winService = ServiceContainer.GetService<IWindowService>();
            var mainWorkspace = new MainWorkspace();
            winService.OpenWindow("mainWorkspace", mainWorkspace, null);
        }
    }
}
