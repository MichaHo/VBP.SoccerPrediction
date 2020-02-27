using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoccerPrediction.ViewModel
{
    public class MainWorkspace : ViewModelBase
    {

        private LoginViewModel _loginVm;
        public LoginViewModel LoginVm { get => _loginVm; set => SetValue(ref _loginVm, value, nameof(LoginVm)); }


        /// <summary>
        /// Kommentar 
        /// </summary>
        private SideMenuViewModel _sideMenuVm;
        public SideMenuViewModel SideMenuVm { get => _sideMenuVm; set => SetValue(ref _sideMenuVm, value, nameof(SideMenuVm)); }


        public MainWorkspace()
        {
            LoginVm = new LoginViewModel("Login Seite (Main Control)");
            SideMenuVm = new SideMenuViewModel();
        }
    }
}
