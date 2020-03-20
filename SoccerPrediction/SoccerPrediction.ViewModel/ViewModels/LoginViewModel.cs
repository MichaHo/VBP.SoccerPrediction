using SoccerPrediction.BusinessLogic;
using SoccerPrediction.Helper;
using SoccerPrediction.Model;
using SoccerPrediction.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SoccerPrediction.ViewModel
{
    public class LoginViewModel : ViewModelBase, IViewModelValidation, IDataErrorInfo
    {
        private readonly PeopleLogic _peopleLogic;
        /// <summary>
        /// der Username der Person
        /// </summary>
        private string _userName;
        public string UserName { get => _userName; set => SetValue(ref _userName, value); }

        private string _password;
        public string Password { get => _password; set => SetValue(ref _password, value); }


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
            _peopleLogic = new PeopleLogic();
            LogInCommand = new RelayCommand(async (param) => await LoginAsync(param));
        }

        public LoginViewModel(string displayText)
        {
            _displayText = displayText;
            _peopleLogic = new PeopleLogic();
            LogInCommand = new RelayCommand(async (param) => await LoginAsync(param));
        }
        
        public LoginViewModel(string displayText,PeopleLogic bl)
        {
            _displayText = displayText;
            _peopleLogic = bl;
            LogInCommand = new RelayCommand(async (param) => await LoginAsync(param));
        }

        #region Command Methods

        /// <summary>
        /// den User einloggen
        /// </ summary>
        /// <param name = "parameter"> der <see cref = "SecureString" />, der aus der View für das Benutzerkennwort übergeben wurde </ param> 
        /// <returns> </ returns>
        public async Task LoginAsync(object parameter)
        {
            await RunCommandAsync(() => VmIsBusy, async () =>
            {
                var user = UserName;
                var pass = (parameter as IHavePassword).SecurePassword.Unsecure();
                //TODO: PeopleLogic erstellen und Methode für das auslesen einer Person mit übergebenen Zugangsdaten erstellen
                Person person =  _peopleLogic.Get(user, pass);
                if (person != null)
                {
                    // Erfolgreich
                    //TODO: Person in eine Instanz schreiben, damit Zugriff auf Personen Infos besteht
                    IWindowService _winService = ServiceContainer.GetService<IWindowService>();
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
        public bool CanLogin()
        {
            ///Die BL fragen ob UserName und Kennwort korrekt sind (die entschlüsselung am besten auch über z.b. einen Helper im BL Projekt machen lassen.
            ///Eine entschlüsselung ist ja auch logik und gehört also dort hin.
            
            var person = _peopleLogic.Get(UserName, Password);
            return person != null;
        }

        #region "IViewModelValidation"
        public List<ValidationResult> ValidationErrors()
        {
            List<ValidationResult> valRet = new List<ValidationResult>();
            if (((Password == null) || (Password.Length == 0)))
            {
                valRet.Add(new ValidationResult("Es muss ein Passwort angegeben werden", new List<string>() { nameof(Password) }));
            }

            if (((UserName == null) || (UserName.Length == 0)))
            {
                valRet.Add(new ValidationResult("Bitte Username angeben", new List<string>() { nameof(UserName) }));
            }
            if (((UserName == null) || (UserName.Length < 3)))
            {
                valRet.Add(new ValidationResult("Username ist zu kurz", new List<string>() { nameof(UserName) }));
            }

            return valRet;
        }

        public bool IsValid => ValidationErrors().Any();
        #endregion


        #region "IDataErrorInfo"
        public string Error => null; //Wird nur z.b. bei DataGrid und der gleichen benötigt und auch nur dort abgferufen

        public string this[string columnName]
        {
            get
            {
                ValidationResult valRes = ValidationErrors().Where(v => v.MemberNames.Contains(columnName) == true).FirstOrDefault();
                if (valRes == null) return null;
                return valRes.ErrorMessage;
            }
        }
        #endregion

    }
    }
