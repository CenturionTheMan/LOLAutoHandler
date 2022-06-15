
using LOL_AutoHandler_v2.Models;
using LOL_AutoHandler_v2.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LOL_AutoHandler_v2.ViewModels
{
    public class ServiceSettingsViewModel : ObservableObject
    {
        public static Action <int>OnCurrentLaneIndexChanged;

        public Champion[] AllChampions
        {
            get { return _allChampions; }
            set { OnPropertyChanged(ref _allChampions, value); }            
        }
        public Champion[,] ToPickChampions
        {
            get { return MainViewModel.instance.ToPickChampions; }
            set { MainViewModel.instance.ToPickChampions = value;}
        }
        public Champion[,] ToBanChampions
        {
            get { return MainViewModel.instance.ToBanChampions; }
            set { MainViewModel.instance.ToBanChampions = value; }
        }
        
        private Champion[] _allChampions;


        public UserControl TopUserControl
        {
            get { return _topUserControl; }
            set { OnPropertyChanged(ref _topUserControl, value); }
        }
        private UserControl _topUserControl;

        public int LaneIndex
        {
            get { return _laneIndex; }
            set { OnPropertyChanged(ref _laneIndex, value); }
        }
        private int _laneIndex = 0;

        public ICommand SetLaneIndexCommand { get; private set; }
        public void SetLaneIndex(int index)
        {
            LaneIndex = index;
            TapedLaneIndex = index;
            OnCurrentLaneIndexChanged?.Invoke(index);
        }

        public static int TapedLaneIndex = 0;
        //CTOR
        public ServiceSettingsViewModel(Champion[] champions)
        {
            AllChampions = champions;
            SetLaneIndexCommand = new RelayCommand<int>(SetLaneIndex);
            TopUserControl = new Service_TopView(this);
        }
    }
}
