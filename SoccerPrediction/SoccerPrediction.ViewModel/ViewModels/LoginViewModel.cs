using SoccerPrediction.Helper;
using SoccerPrediction.Model;
using SoccerPrediction.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SoccerPrediction.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        /// <summary>
        /// Instanz des <see cref="IWindowService"/> erstellen und Service holen
        /// </summary>
        private readonly IWindowService _winService = ServiceContainer.GetService<IWindowService>();

        /// <summary>
        /// der Username der Person
        /// </summary>
        private string _userName;
        public string UserName { get => _userName; set => SetValue(ref _userName, value); }

        /// <summary>
        /// gibt an ob der Login gestartet ist
        /// kann verwendet werden um in der View anzuzeigen das etwas passiert
        /// </summary>
        private bool _loginIsRunning;
        public bool LoginIsRunning { get => _loginIsRunning; set => SetValue(ref _loginIsRunning, value); }


        private string _displayText;
        public string DisplayText { get => _displayText; set => SetValue(ref _displayText, value); }

        #region Commands

        /// <summary>
        /// der Command zum einloggen
        /// </summary>
        public ICommand LogInCommand { get; private set; }

        /// <summary>
        /// der Command zum registrieren
        /// kann genutzt werden wenn die Möglichkeit der Registrierung direkt in der App erfolgen soll
        /// </summary>
        public ICommand RegisterCommand { get; private set; }

        #endregion


        public LoginViewModel()
        {
            _displayText = "Login Seite";
            LogInCommand = new RelayCommand(async (param) => await LoginAsync(param));
        }

        public LoginViewModel(string displayText)
        {
            _displayText = displayText;
            LogInCommand = new RelayCommand( async (param) => await LoginAsync(param));
        }

        #region Command Methods

        /// <summary>
        /// den User einloggen
        /// </ summary>
        /// <param name = "parameter"> der <see cref = "SecureString" />, der aus der View für das Benutzerkennwort übergeben wurde </ param> 
        /// <returns> </ returns>
        public async Task LoginAsync(object parameter)
        {
            await RunCommandAsync(() => LoginIsRunning, async () =>
            {
                var user = UserName;
                var pass = (parameter as IHavePassword).SecurePassword.Unsecure();
                //TODO: PeopleLogic erstellen und Methode für das auslesen einer Person mit übergebenen Zugangsdaten erstellen
                Person person = new Person(); //await _peopleLogic.GetPersonAsync(user, pass);
                if (person != null)
                {
                    // Erfolgreich
                    //TODO: Person in eine Instanz schreiben, damit Zugriff auf Personen Infos besteht
                    _winService.OpenWindow("mainWindow", new MainWorkspace(), null);

                }
                else
                {
                    // Nicht erfolgreich
                    //TODO: Fehlerbehandlung
                    //await _dlgService.ShowDialogAsync(this, "Falsche Zugangsdaten", "Ihre Zugangsdaten waren nicht korrekt, bitte versuchen Sie es erneut");
                }
            });
        }
        #endregion

    }
}
