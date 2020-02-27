using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerPrediction.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {

        private string _displayText;
        public string DisplayText { get => _displayText; set => SetValue(ref _displayText, value); }


        public LoginViewModel()
        {
            DisplayText = "Login Seite";
        }

        public LoginViewModel(string displayText)
        {
            DisplayText = displayText;
        }
    }
}
