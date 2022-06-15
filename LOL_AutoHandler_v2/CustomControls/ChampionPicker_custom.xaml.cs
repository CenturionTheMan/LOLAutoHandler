using LOL_AutoHandler_v2.Models;
using LOL_AutoHandler_v2.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LOL_AutoHandler_v2.CustomControls
{
    /// <summary>
    /// Interaction logic for ChampionPicker_custom.xaml
    /// </summary>
    public partial class ChampionPicker_custom : UserControl
    {
        public Champion[] CustomItemSource
        {
            get { return (Champion[])GetValue(CustomItemSourceProperty); }
            set { SetValue(CustomItemSourceProperty, value); }
        }
        public static readonly DependencyProperty CustomItemSourceProperty =
            DependencyProperty.Register("CustomItemSource", typeof(Champion[]), typeof(ChampionPicker_custom));



        public int TargetLaneIndex
        {
            get { return (int)GetValue(TargetLaneIndexProperty); }
            set { SetValue(TargetLaneIndexProperty, value); }
        }
        public static readonly DependencyProperty TargetLaneIndexProperty =
            DependencyProperty.Register("TargetLaneIndex", typeof(int), typeof(ChampionPicker_custom), new PropertyMetadata(0));


        public int TargetOrderIndex
        {
            get { return (int)GetValue(TargetOrderIndexProperty); }
            set { SetValue(TargetOrderIndexProperty, value); }
        }
        public static readonly DependencyProperty TargetOrderIndexProperty =
            DependencyProperty.Register("TargetOrderIndex", typeof(int), typeof(ChampionPicker_custom), new PropertyMetadata(0));


        public bool IsBan
        {
            get { return (bool)GetValue(IsBanProperty); }
            set { SetValue(IsBanProperty, value); }
        }
        public static readonly DependencyProperty IsBanProperty =
            DependencyProperty.Register("IsBan", typeof(bool), typeof(ChampionPicker_custom), new PropertyMetadata(false));


        public double ImageSize
        {
            get { return (double)GetValue(ImageSizeProperty); }
            set { SetValue(ImageSizeProperty, value); }
        }
        public static readonly DependencyProperty ImageSizeProperty =
            DependencyProperty.Register("ImageSize", typeof(double), typeof(ChampionPicker_custom));


        public double TextSize
        {
            get { return (double)GetValue(TextSizeProperty); }
            set { SetValue(TextSizeProperty, value); }
        }
        public static readonly DependencyProperty TextSizeProperty =
            DependencyProperty.Register("TextSize", typeof(double), typeof(ChampionPicker_custom));



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ChampionPicker_custom), new PropertyMetadata(""));



        public ChampionPicker_custom()
        {
            InitializeComponent();

            ServiceSettingsViewModel.OnCurrentLaneIndexChanged += SetBgImage;
        }


        private void RadioButton_custom_JoinClick(object sender, RoutedEventArgs e)
        {
            var bt = (RadioButton_ChampionPicker_custom)sender;
            Champion champ = bt.DataContext as Champion;

            expanderObj.IsExpanded = false;
            if (IsBan == false) MainViewModel.instance.ToPickChampions[TargetLaneIndex, TargetOrderIndex] = champ;
            else
                MainViewModel.instance.ToBanChampions[TargetLaneIndex, TargetOrderIndex] = champ;

            SetBgImage(0);
        }

        private void expanderObj_Expanded(object sender, RoutedEventArgs e)
        {
            
        }

        private void root_Loaded(object sender, RoutedEventArgs e)
        {
            SetBgImage(0);
        }

        void SetBgImage(int i)
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                if (MainViewModel.instance.ToPickChampions[TargetLaneIndex, TargetOrderIndex] == null) return;

                BitmapImage b = new BitmapImage();
                b.BeginInit();
                if (IsBan) b.UriSource = new Uri(MainViewModel.instance.ToBanChampions[TargetLaneIndex, TargetOrderIndex].UIPicture, UriKind.Relative);
                else
                    b.UriSource = new Uri(MainViewModel.instance.ToPickChampions[TargetLaneIndex, TargetOrderIndex].UIPicture, UriKind.Relative);
                b.EndInit();

                image.Source = b;
            }
            
        }


        private bool JustChecked;
        private void HeaderSite_Click(object sender, RoutedEventArgs e)
        {
            if (JustChecked)
            {
                JustChecked = false;
                e.Handled = true;
                return;
            }
            RadioButton s = sender as RadioButton;
            if (s.IsChecked == true)
                s.IsChecked = false;
        }

        private void HeaderSite_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton s = sender as RadioButton;
            // Action on Check...
            JustChecked = true;
        }
    }
}
