using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoccerPrediction.ViewModel
{
    public class MainWorkspace : ViewModelBase
    {
        private List<object> _availableContents;
        public List<object> AvailableContents { get => _availableContents; set => SetValue(ref _availableContents, value); }


        private object _currentContent;
        public object CurrentContent { get => _currentContent; set => SetValue(ref _currentContent, value); }

        public MainWorkspace()
        {
            AvailableContents = new List<object>
            {
                new LoginViewModel()
            };
            CurrentContent = AvailableContents.First();
        }
    }
}
