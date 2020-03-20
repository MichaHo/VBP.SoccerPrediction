using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SoccerPrediction.Context
{
    [Serializable]
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Property Helper

        protected bool SetValue<T>(ref T passedValue, T value, [CallerMemberName] string propertyname = null)
        {
            // prüfen ob value und das übergebene value unterschiedlich sind
            if (EqualityComparer<T>.Default.Equals(passedValue, value))
            {
                return false;
            }

            // value setzen und OnPropertyChanged aufrufen
            passedValue = value;
            OnPropertyChanged(propertyname);
            return true;
        }

        #endregion
    }
}
