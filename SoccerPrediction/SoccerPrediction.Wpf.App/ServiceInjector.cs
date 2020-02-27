using SoccerPrediction.ViewModel;
using SoccerPrediction.ViewModel.Services;

namespace SoccerPrediction.Wpf.App
{
    public sealed class ServiceInjector
    {
        private ServiceInjector()
        { }

        public static void InjectServices()
        {
            ServiceContainer.ServiceInstance.AddService<IWindowService>(new WindowService());
        }
    }
}
