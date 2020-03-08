using SoccerPrediction.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SoccerPrediction.UnitTests.ViewModel
{
    class LoginWindowServiceMock : IWindowService
    {
        public void CloseWindow()
        {
            Debug.WriteLine($"Closing Window (Mock)");
        }

        public void CloseWindow(object vm)
        {
            Debug.WriteLine($"Closing Window (Mock) with parameter '{vm.ToString()}'");
        }

        public bool OpenWindow(string windowName, object dataContext, object owner, bool topMost = false, bool showInTaskbar = true)
        {
            return true;
        }
    }
}
