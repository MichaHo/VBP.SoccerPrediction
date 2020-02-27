using SoccerPrediction.View;
using SoccerPrediction.ViewModel.Services;
using System;
using System.Linq;
using System.Windows;

namespace SoccerPrediction.Wpf.App
{
    public class WindowService : IWindowService
    {
        private AppWindow _win;
        public void CloseWindow()
        {
            if (_win != null) _win.Close();
            else throw new Exception("Win was nothing!!!");
        }

        public void CloseWindow(object vm)
        {
            Window owner = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.DataContext.GetType() == vm.GetType());
            if (owner == null)
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.DataContext.GetType() == vm.GetType()) window.Close();
                }
            }
            else
                owner.Close();
        }

        public bool OpenWindow(string windowName, object dataContext, object owner, bool topMost = false, bool showInTaskbar = true)
        {
            var win = new AppWindow(windowName, dataContext, SizeToContent.Manual, WindowStartupLocation.CenterOwner) { Topmost = topMost, ShowInTaskbar = showInTaskbar };
            win.Owner = win.FindOwnerWindow(owner);
            win.AsModalDialog = false;
            Application.Current.Dispatcher.Invoke(() => win.Show());
            _win = win;
            return true;
        }
    }
}
