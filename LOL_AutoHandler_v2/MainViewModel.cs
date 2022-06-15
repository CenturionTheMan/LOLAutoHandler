using LOL_AutoHandler_v2.CustomControls;
using LOL_AutoHandler_v2.Models;
using LOL_AutoHandler_v2.Python;
using LOL_AutoHandler_v2.Utilty;
using LOL_AutoHandler_v2.ViewModels;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LOL_AutoHandler_v2
{
    public class MainViewModel : ObservableObject
    {
        #region PROPERTIES
        //  TO WORK-ON VALUES
        public Champion[] AllChampions
        {
            get { return _allChampions; }
            set { OnPropertyChanged(ref _allChampions, value); }
        }
        public Champion[,] ToPickChampions
        {
            get { return _toPickChampions; }
            set { OnPropertyChanged(ref _toPickChampions, value);}
        }

        public Champion[,] ToBanChampions
        {
            get { return _toBanChampions; }
            set { OnPropertyChanged(ref _toBanChampions, value); }
        }
        
        public WindowSize ClientWindowSize = WindowSize.OTHER;
        public ClientState CurrentClientState = ClientState.NONE;
        public Vector2 WindowPosition;

        public bool IsServiceOn
        {
            get { return _isServiceOn; }
            set 
            { 
                OnPropertyChanged(ref _isServiceOn, value);
                if (value) _serviceLogic.RunLogic();
                else if (_serviceLogic.MainThread != null) _serviceLogic.AbortAndCleanup();
            }
        }
        public bool IsBanService
        {
            get { return _isBanService; }
            set { OnPropertyChanged(ref _isBanService, value); }
        }
        public bool IsPickService
        {
            get { return _isPickService; }
            set { OnPropertyChanged(ref _isPickService, value); }
        }

        // VIEWS && VIEWMODELS
        public object CurrentView
        {
            get { return _currentView; }
            set { OnPropertyChanged(ref _currentView, value); }
        }
        public StartViewModel StartVM
        {
            get { return _startVM; }
            set { OnPropertyChanged(ref _startVM, value); }
        }
        public ServiceSettingsViewModel ServiceSettingsVM
        {
            get { return _serviceSettingsVM; }
            set { OnPropertyChanged(ref _serviceSettingsVM, value); }
        }

        //  PRIVATE VALUES
        private object _currentView;
        private StartViewModel _startVM;
        private ServiceSettingsViewModel _serviceSettingsVM;
        private ServiceLogic _serviceLogic;

        private Champion[] _allChampions;
        private Champion[,] _toPickChampions;
        private Champion[,] _toBanChampions;

        private bool _isServiceOn = false;
        private bool _isBanService = false;
        private bool _isPickService = false;

        private HandlePython _handlePython;
        #endregion

        #region COMMANDS
        //  COMMANDS
        public ICommand ChangeActiveViewCommand { get; private set; }
        public ICommand CloseAppCommand { get; private set; }
        public ICommand MinimizeAppCommand { get; private set; }

        //BODIES
        public void ChangeActiveView(object view)
        {
            CurrentView = view;
        }

        public void CloseApp()
        {
            IsServiceOn = false;

            JasonHandler.SaveChampionstToJson(_toPickChampions, Helpers.GetProjectDirectory(@"Data\ToPickChampions.json"));
            JasonHandler.SaveChampionstToJson(_toBanChampions, Helpers.GetProjectDirectory(@"Data\ToBanChampions.json"));

            Application.Current.MainWindow.Close();
        }

        public void MinimizeApp()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        #endregion




        public static MainViewModel instance;
        //-------------------------------------------CTOR
        public MainViewModel()
        {
            instance = this;

            _handlePython = new HandlePython();
            _serviceLogic = new ServiceLogic(this, _handlePython);

            SetUpChampions();

            StartVM = new StartViewModel();
            ServiceSettingsVM = new ServiceSettingsViewModel(AllChampions);

            ChangeActiveViewCommand = new RelayCommand<object>(ChangeActiveView);
            CloseAppCommand = new RelayCommand(CloseApp);
            MinimizeAppCommand = new RelayCommand(MinimizeApp);

            CurrentView = StartVM;
        }

        void SetUpChampions()
        {
            AllChampions = Helpers.ReadChampionsFormJson(Helpers.GetProjectDirectory(@"Python\ChampionsData.json"));

            ToPickChampions = JasonHandler.ReadChampionsFromFile(Helpers.GetProjectDirectory(@"Data\ToPickChampions.json"));
            ToBanChampions = JasonHandler.ReadChampionsFromFile(Helpers.GetProjectDirectory(@"Data\ToBanChampions.json"));


            if (ToPickChampions == null)
            {
                ToPickChampions = new Champion[5, 5];
                for (int i = 0; i < ToPickChampions.GetLongLength(0); i++)
                {
                    for (int j = 0; j < ToPickChampions.GetLongLength(1); j++)
                    {
                        ToPickChampions[i, j] = new Champion();
                        ToPickChampions[i, j].Name = "None";
                        ToPickChampions[i, j].UIPicture = "None";
                    }
                }
            }

            if (ToBanChampions == null)
            {
                ToBanChampions = new Champion[5, 5];
                for (int i = 0; i < ToBanChampions.GetLongLength(0); i++)
                {
                    for (int j = 0; j < ToBanChampions.GetLongLength(1); j++)
                    {
                        ToBanChampions[i, j] = new Champion();
                        ToBanChampions[i, j].Name = "None";
                        ToBanChampions[i, j].UIPicture = "None";
                    }
                }
            }
        }

    }
}
