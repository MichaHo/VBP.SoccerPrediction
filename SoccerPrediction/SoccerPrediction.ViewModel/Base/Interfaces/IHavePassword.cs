using System.Security;

namespace SoccerPrediction.ViewModel
{
    public interface IHavePassword
    {
        SecureString SecurePassword { get; }
    }
}
