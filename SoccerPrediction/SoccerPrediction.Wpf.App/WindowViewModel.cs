using SoccerPrediction.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace SoccerPrediction.Wpf.App
{
    public class WindowViewModel : ViewModelBase
    {
        private readonly Window _window;

        #region Commands

        public ICommand MinimizeCommand { get; private set; }
        public ICommand MaximizeCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }

        #endregion


        public WindowViewModel(Window window)
        {
            _window = window;
            _window.MouseLeftButtonDown += _window_MouseLeftButtonDown;
            WindowTitle = "Soccer Prediction";            
            MinimizeCommand = new RelayCommand(m => _window.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(m => _window.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(c => _window.Close());
        }

        private void _window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _window.DragMove();
        }
    }
}
