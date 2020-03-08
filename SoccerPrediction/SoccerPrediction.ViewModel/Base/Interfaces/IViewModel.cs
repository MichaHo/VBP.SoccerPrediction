using System;
using System.ComponentModel;

namespace SoccerPrediction.ViewModel
{
    public interface IViewModel :
        INotifyPropertyChanged,
        IDisposable
    { }

    public interface IViewModel<TModel> : IViewModel
    {
        [Browsable(false)]
        [Bindable(false)]
        TModel Model { get; set; }
    }
}
