namespace SoccerPrediction.ViewModel.Services
{
    public interface IWindowService
    {
        bool OpenWindow(string windowName, object dataContext, object owner, bool topMost = false, bool showInTaskbar = true);
        void CloseWindow();
        void CloseWindow(object vm);
    }
}
