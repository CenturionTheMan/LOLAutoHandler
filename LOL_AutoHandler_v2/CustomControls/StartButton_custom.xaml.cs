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
    public partial class StartButton_custom : UserControl
    {

        public Brush BaseBackgroungColor
        {
            get { return (Brush)GetValue(BaseBackgroungColorProperty); }
            set { SetValue(BaseBackgroungColorProperty, value); }
        }
        public static readonly DependencyProperty BaseBackgroungColorProperty =
            DependencyProperty.Register("BaseBackgroungColor", typeof(object), typeof(StartButton_custom), new PropertyMetadata(Brushes.Black));



        public Brush BaseForegroundColor
        {
            get { return (Brush)GetValue(BaseForegroundColorProperty); }
            set { SetValue(BaseForegroundColorProperty, value); }
        }
        public static readonly DependencyProperty BaseForegroundColorProperty =
            DependencyProperty.Register("BaseForegroundColor", typeof(object), typeof(StartButton_custom), new PropertyMetadata(Brushes.White));



        public Brush OnHoverForeground
        {
            get { return (Brush)GetValue(OnHoverForegroundProperty); }
            set { SetValue(OnHoverForegroundProperty, value); }
        }
        public static readonly DependencyProperty OnHoverForegroundProperty =
            DependencyProperty.Register("OnHoverForeground", typeof(object), typeof(StartButton_custom), new PropertyMetadata(Brushes.Yellow));



        public Brush OnClickForeground
        {
            get { return (Brush)GetValue(OnClickForegroundProperty); }
            set { SetValue(OnClickForegroundProperty, value); }
        }
        public static readonly DependencyProperty OnClickForegroundProperty =
            DependencyProperty.Register("OnClickForeground", typeof(object), typeof(StartButton_custom), new PropertyMetadata(Brushes.Gold));



        public double TextSize
        {
            get { return (double)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("TextSize", typeof(object), typeof(StartButton_custom), new PropertyMetadata(10));




        public new double BorderThickness
        {
            get { return (double)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }
        public static new readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(object), typeof(StartButton_custom), new PropertyMetadata(1));



        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }
        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register("ButtonText", typeof(object), typeof(StartButton_custom), new PropertyMetadata("NONE"));



        public ICommand CustomCommand
        {
            get { return (ICommand)GetValue(CustomCommandProperty); }
            set { SetValue(CustomCommandProperty, value); }
        }
        public static readonly DependencyProperty CustomCommandProperty =
            DependencyProperty.Register("CustomCommand", typeof(ICommand), typeof(StartButton_custom), new PropertyMetadata(new RelayCommand(Temp)));




        public object CustomCommandParameter
        {
            get { return (object)GetValue(CustomCommandParameterProperty); }
            set { SetValue(CustomCommandParameterProperty, value); }
        }
        public static readonly DependencyProperty CustomCommandParameterProperty =
            DependencyProperty.Register("CustomCommandParameter", typeof(object), typeof(StartButton_custom), new PropertyMetadata(null));




        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            set { SetValue(IsPressedProperty, value); }
        }
        public static readonly DependencyProperty IsPressedProperty =
            DependencyProperty.Register("IsPressed", typeof(bool), typeof(StartButton_custom), new PropertyMetadata(false));





        static void Temp()
        {
            //MessageBox.Show("Not implemented");
        }





        public StartButton_custom()
        {
            InitializeComponent();
        }
    }
}
