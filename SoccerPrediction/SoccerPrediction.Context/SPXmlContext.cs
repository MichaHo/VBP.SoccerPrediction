using SoccerPrediction.Helper;
using SoccerPrediction.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace SoccerPrediction.Context
{
    [Serializable]
    public class SPXmlContext : IDisposable
    {
        #region Private Members

        private readonly string _xmlPath;
        private const string LockFileName = "lock.loc";
        private DateTime _lastSyncTime;

        private FileSystemWatcher _xmlFileWatcher;
        private XmlContext CurrentContext { get; set; }

        #endregion

        #region Internal Delegates

        internal delegate void DataFileWasChangedEventHandler();
        internal event DataFileWasChangedEventHandler DataFileWasChanged;

        internal delegate void ErrorMessageSendEventHandler(string message);
        internal event ErrorMessageSendEventHandler ErrorMessageSend;

        #endregion

        #region Constructor

        public SPXmlContext()
        {
            //TODO: Pfad in den Settings abfragen und ggf. eintragen
            if(string.IsNullOrEmpty(_xmlPath)) throw new Exception("Der Pfad zum XML-Datenfile muss in den Settings hinterlegt sein.");
            if(!File.Exists(_xmlPath)) throw new Exception("Der Pfad zur XML-Datendatei aus den Settings existiert nicht.");
            _xmlPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "xmlFile.xml");
            InitSPContext();
        }

        public SPXmlContext(string xmlPath, bool savePathToSettings = false)
        {
            if(!Directory.Exists(Path.GetDirectoryName(xmlPath))) throw new Exception("Der Pfad zum XML-Datendateiordner existiert nicht.");
            _xmlPath = xmlPath;
            //TODO: abfragen ob Pfad in Settings gespeichert werden soll und ggf. ablegen
            InitSPContext();
        }

        #endregion

        #region XML Sets

        public ObservableCollection<AccessData> AccessDatas => CurrentContext.AccessDatas;
        public ObservableCollection<Encounter> Encounters => CurrentContext.Encounters;
        public ObservableCollection<GameDay> GameDays => CurrentContext.GameDays;
        public ObservableCollection<Person> People => CurrentContext.People;
        public ObservableCollection<Prediction> Predictions => CurrentContext.Predictions;
        public ObservableCollection<Team> Teams => CurrentContext.Teams;

        #endregion

        #region Public Properties

        public bool IsDataUpToDate => new FileInfo(_xmlPath).LastWriteTime <= _lastSyncTime;
        public bool HasPessimisticLock => File.Exists($@"{Path.GetDirectoryName(_xmlPath)}\{LockFileName}");

        #endregion

        #region Public Methods

        public bool SaveChanges()
        {
            if (HasPessimisticLock) ErrorMessageSend?.Invoke("Die Datei ist gesperrt und kann nicht gespeichert werden!");
            try
            {
                _xmlFileWatcher.EnableRaisingEvents = false;
                LockForChanges();
                XmlSerializer serializer = new XmlSerializer();
                serializer.Serialize<XmlContext>(_xmlPath, CurrentContext);
                _lastSyncTime = DateTime.Now;
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessageSend?.Invoke(ex.Message);
                return false;
            }
            finally
            {
                UnlockForChanges();
                _xmlFileWatcher.EnableRaisingEvents = true;
            }
        }

        #endregion

        #region Private Methods

        private void InitSPContext()
        {
            LoadContext();
            SetupFileWatcher();
        }

        private void SetupFileWatcher()
        {
            _xmlFileWatcher = new FileSystemWatcher(Path.GetDirectoryName(_xmlPath));
            _xmlFileWatcher.Changed += DataFileChanged;
            _xmlFileWatcher.Filter = Path.GetDirectoryName(_xmlPath);
            _xmlFileWatcher.EnableRaisingEvents = true;
        }

        private void DataFileChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
                DataFileWasChanged?.Invoke();
        }

        private void LockForChanges()
        {
            using (FileStream fs = new FileStream($@"{Path.GetDirectoryName(_xmlPath)}\{LockFileName}", FileMode.Create))
            {
                fs.Close();
            }
        }

        private void UnlockForChanges()
        {
            File.Delete($@"{Path.GetDirectoryName(_xmlPath)}\{LockFileName}");
        }
        #endregion

        #region internal Methods

        internal void LoadContext()
        {
            XmlSerializer serializer = new XmlSerializer();
            CurrentContext = serializer.DeSerialize<XmlContext>(_xmlPath, new XmlContext());
            _lastSyncTime = DateTime.Now;
        }

        public void Seed()
        {
            LockForChanges();
            if(!People.Any())
            {
                People.Add(new Person() { FirstName = "Hans", LastName = "Muster", Credentials = new AccessData() { UserName = "hmuster", EncryptedPassword = "pass".GetHash(), IsAdmin = true } });
                People.Add(new Person() { FirstName = "Herbert", LastName = "Muster2", Credentials = new AccessData() { UserName = "hmuster2", EncryptedPassword = "pass2".GetHash(), IsAdmin = false } });
            }
            if(Teams.Any())
            {
                Teams.Add(new Team() { Name = "Eintracht Frankfurt" });
                Teams.Add(new Team() { Name = "Schalke 04" });
                Teams.Add(new Team() { Name = "Borussia Dortmund" });
            }
            UnlockForChanges();
            SaveChanges();
        }

        #endregion

        #region IDisposable

        private bool _disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if(!_disposedValue)
            {
                if(disposing)
                {
                    UnlockForChanges();
                    CurrentContext = null;
                    _xmlFileWatcher.Changed -= DataFileChanged;
                }
            }
            _disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }

    [Serializable]
    public class XmlContext : NotifyPropertyChanged
    {
        public XmlContext()
        {
            AccessDatas = new ObservableCollection<AccessData>();
            Encounters = new ObservableCollection<Encounter>();
            GameDays = new ObservableCollection<GameDay>();
            People = new ObservableCollection<Person>();
            Predictions = new ObservableCollection<Prediction>();
            Teams = new ObservableCollection<Team>();
        }

        private ObservableCollection<AccessData> _allAccessData;
        public ObservableCollection<AccessData> AccessDatas { get => _allAccessData; set => SetValue(ref _allAccessData, value); }

        private ObservableCollection<Encounter> _allEncounters;
        public ObservableCollection<Encounter> Encounters { get => _allEncounters; set => SetValue(ref _allEncounters, value); }

        private ObservableCollection<GameDay> _allGameDays;
        public ObservableCollection<GameDay> GameDays { get => _allGameDays; set => SetValue(ref _allGameDays, value); }

        private ObservableCollection<Person> _allPeople;
        public ObservableCollection<Person> People { get => _allPeople; set => SetValue(ref _allPeople, value); }

        private ObservableCollection<Prediction> _allPredictions;
        public ObservableCollection<Prediction> Predictions { get => _allPredictions; set => SetValue(ref _allPredictions, value); }

        private ObservableCollection<Team> _allTeams;
        public ObservableCollection<Team> Teams { get => _allTeams; set => SetValue(ref _allTeams, value); }


    }
}
