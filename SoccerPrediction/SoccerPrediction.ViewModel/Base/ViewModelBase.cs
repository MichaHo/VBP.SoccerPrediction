using SoccerPrediction.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SoccerPrediction.ViewModel
{
    public abstract class ViewModelBase : IViewModel
    {

        #region Public Properties

        private static readonly List<string> HostProcesses = new List<string> { "XDesProc", "devenv", "WDExpress", "WpfSurface" };
        public bool IsInDesignMode => HostProcesses.Contains(Process.GetCurrentProcess().ProcessName);

        private string _windowTitle;
        public string WindowTitle { get => _windowTitle; set => SetValue(ref _windowTitle, value); }

        private bool _vmIsBusy;
        public bool VmIsBusy { get => _vmIsBusy; set => SetValue(ref _vmIsBusy, value); }
        public virtual bool IsValid => !ValidationErrors().Any();

        #endregion

        #region Constructor

        public ViewModelBase()
        {
            var iniTask = new Task(() => Initialize());
            iniTask.ContinueWith(result => InitializationCompletedCallback(result));
            iniTask.Start();
        }

        #endregion

        #region Initialization
        private void InitializationCompletedCallback(IAsyncResult result)
        {
            var iniCompleted = InitializationCompleted;
            if (iniCompleted != null)
                InitializationCompleted(this, new AsyncCompletedEventArgs(null, !result.IsCompleted, result.AsyncState));
            InitializationCompleted = null;
        }

        public event AsyncCompletedEventHandler InitializationCompleted;
        protected virtual void Initialize()
        { }
        #endregion

        #region IDataErrorInfo

        public virtual List<ValidationResult> ValidationErrors()
        {
            return null;
        }

        public string this[string columnName]
        {
            get
            {
                ValidationResult valRes = ValidationErrors().Where(v => v.MemberNames.Contains(columnName) == true).FirstOrDefault();
                if (valRes == null) return null;
                return valRes.ErrorMessage;
            }
        }

        public virtual string Error => string.Empty;

        #endregion

        #region IDisposable

        private bool _disposedValue;
        public virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing) { }
            }
            _disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

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

        #region Command Helpers

        /// <summary>
        /// führt einen Befehl aus, wenn das Aktualisierungsflag nicht gesetzt ist
        /// Wenn das Flag wahr ist (was darauf hinweist, dass die Funktion bereits ausgeführt wird), wird die Aktion nicht ausgeführt
        /// Wenn das Flag falsch ist (was anzeigt, dass keine Funktion ausgeführt wird), wird die Aktion ausgeführt
        /// Wenn die Aktion beendet ist, wenn sie ausgeführt wird, wird das Flag zurückgesetzt, sobald sie ausgeführt wurde
        /// </ summary>
        /// <param name = "updatedFlag"> das Flag, das definiert, ob der Befehl bereits ausgeführt wird </ param>
        /// <param name = "action"> die auszuführende Aktion, wenn der Befehl noch nicht ausgeführt wird </ param>
        protected async Task RunCommandAsync(Expression<Func<bool>> updatingFlag, Func<Task> action)
        {
            if (updatingFlag.GetPropertyValue())
                return;

            updatingFlag.SetPropertyValue(true);

            try
            {
                await action();
            }
            finally
            {
                updatingFlag.SetPropertyValue(false);
            }

        }

        #endregion

    }

    #region ViewModelBase<TModel>

    /// <summary>
    /// Basisklasse für alle ViewModelModel mit Data Model Support
    /// </summary>
    public abstract class ViewModelBase<TModel> : ViewModelBase, IViewModel<TModel> where TModel : class
    {
        private TModel _model;
        public TModel Model
        {
            get => _model;
            set
            {
                if (Model != value)
                {
                    _model = value;
                }
            }
        }

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="model"></param>
        protected ViewModelBase(TModel model)
        {
            this._model = model;
        }

        public override int GetHashCode()
        {
            return Model.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var other = obj as IViewModel<TModel>;
            if (other == null) return false;
            return Equals(other);
        }

        public bool Equals(IViewModel<TModel> other)
        {
            if (other == null) return false;
            if (Model == null) return Model == other.Model;
            return Model.Equals(other.Model);
        }
    }
    #endregion
}
