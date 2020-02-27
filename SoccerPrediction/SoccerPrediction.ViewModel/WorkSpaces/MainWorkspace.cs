using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoccerPrediction.ViewModel
{
    public class MainWorkspace : ViewModelBase
    {


        /// <summary>
        /// Kommentar 
        /// </summary>
        private LoginViewModel _loginVm;
        public LoginViewModel LoginVm { get => _loginVm; set => SetValue(ref _loginVm, value); }


        public MainWorkspace()
        {
            LoginVm = new LoginViewModel("Login Seite (Main Control)");
        }
    }
}
