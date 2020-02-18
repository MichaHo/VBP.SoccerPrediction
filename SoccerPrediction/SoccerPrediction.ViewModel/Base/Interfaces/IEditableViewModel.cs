using System.Windows.Input;

namespace SoccerPrediction.ViewModel
{
    public interface IEditableViewModel
    {
        ICommand Save { get; }
        ICommand SaveAndClose { get; }
        ICommand Abort { get; }
        ICommand ShowProtocol { get; }
    }
}
