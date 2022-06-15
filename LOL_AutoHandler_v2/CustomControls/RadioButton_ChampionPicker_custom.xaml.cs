using LOL_AutoHandler_v2.Utilty;
using System;
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
    /// Interaction logic for RadioButton_ChampionPicker_custom.xaml
    /// </summary>
    public partial class RadioButton_ChampionPicker_custom :  UserControl
    {
        public ImageSource OnHoverBg
        {
            get { return (ImageSource)GetValue(OnHoverBgProperty); }
            set { SetValue(OnHoverBgProperty, value); }
        }
        public static readonly DependencyProperty OnHoverBgProperty =
            DependencyProperty.Register("OnHoverBg", typeof(object), typeof(RadioButton_ChampionPicker_custom));


        public ImageSource OnClickBg
        {
            get { return (ImageSource)GetValue(OnClickBgProperty); }
            set { SetValue(OnClickBgProperty, value); }
        }
        public static readonly DependencyProperty OnClickBgProperty =
            DependencyProperty.Register("OnClickBg", typeof(object), typeof(RadioButton_ChampionPicker_custom));

        public ImageSource BaseBg
        {
            get { return (ImageSource)GetValue(BaseBgProperty); }
            set { SetValue(BaseBgProperty, value); }
        }
        public static readonly DependencyProperty BaseBgProperty =
            DependencyProperty.Register("BaseBg", typeof(object), typeof(RadioButton_ChampionPicker_custom));




        public double OnHoverSize
        {
            get { return (double)GetValue(OnHoverSizeProperty); }
            set { SetValue(OnHoverSizeProperty, value); }
        }
        public static readonly DependencyProperty OnHoverSizeProperty =
            DependencyProperty.Register("OnHoverSize", typeof(object), typeof(RadioButton_ChampionPicker_custom));




        public ICommand CustomCommand
        {
            get { return (ICommand)GetValue(CustomCommandProperty); }
            set { SetValue(CustomCommandProperty, value); }
        }
        public static readonly DependencyProperty CustomCommandProperty =
            DependencyProperty.Register("CustomCommand", typeof(ICommand), typeof(RadioButton_ChampionPicker_custom));

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(JoinClickEvent));
        }

        public static readonly RoutedEvent JoinClickEvent = EventManager.RegisterRoutedEvent("JoinClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(RadioButton_ChampionPicker_custom));
        public event RoutedEventHandler JoinClick
        {
            add { AddHandler(JoinClickEvent, value); }
            remove { RemoveHandler(JoinClickEvent, value); }
        }

        public RadioButton_ChampionPicker_custom()
        {
            InitializeComponent();
        }
    }
}
