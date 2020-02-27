using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SoccerPrediction.View
{
    /// <summary>
    /// Interaktionslogik für AppWindow.xaml
    /// </summary>
    public partial class AppWindow : Window
    {
        public object MContext;

        #region Dependecy Properties

        public bool CanCloseWithEsc
        {
            get { return (bool)GetValue(CanCloseWithEscProperty); }
            set { SetValue(CanCloseWithEscProperty, value); }
        }

        public static readonly DependencyProperty CanCloseWithEscProperty =
            DependencyProperty.Register("CanCloseWithEsc", typeof(bool), typeof(AppWindow), new PropertyMetadata(true));



        public bool AsModalDialog
        {
            get { return (bool)GetValue(AsModalDialogProperty); }
            set { SetValue(AsModalDialogProperty, value); }
        }

        public static readonly DependencyProperty AsModalDialogProperty =
            DependencyProperty.Register("AsModalDialog", typeof(bool), typeof(AppWindow), new PropertyMetadata(true));



        #endregion

        #region Constructor

        public AppWindow(string name)
        {
            Name = name;
            this.Loaded += AppWindow_Loaded;
            InitializeComponent();
        }

        public AppWindow(string name, object dataContext, SizeToContent sizeToContent = SizeToContent.WidthAndHeight, WindowStartupLocation startupLocation = WindowStartupLocation.CenterScreen)
        {
            Name = name;
            MContext = dataContext;
            SizeToContent = sizeToContent;
            WindowStartupLocation = startupLocation;
            this.Loaded += AppWindow_Loaded;
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void AppWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PreviewKeyDown += AppWindow_PreviewKeyDown;
            if (AsModalDialog)
            {
                if (Owner != null)
                    ShowInTaskbar = false;
            }
            DataContext = MContext;
            BringIntoView();
        }

        private void AppWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && CanCloseWithEsc) Close();
        }

        public Window FindOwnerWindow(object viewModel)
        {
            if (viewModel != null)
            {
                if (viewModel.GetType() == typeof(AppWindow)) return (Window)viewModel;
                if (Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.DataContext != null && x.DataContext.GetType() == viewModel.GetType()) == null)
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.DataContext != null && window.DataContext.GetType() == viewModel.GetType())
                        {
                            return window;
                        }
                    }
                }
                else
                    return Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.DataContext != null && x.DataContext.GetType() == viewModel.GetType());
            }
            return Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.IsActive);
        }

        public void SetWindowState(bool maximize)
        {
            WindowState = maximize ? WindowState.Maximized : WindowState.Normal;
        }

        #endregion
    }
}
