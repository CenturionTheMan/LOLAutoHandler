using System;
using System.Collections.Generic;
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
    /// Interaction logic for ExampleTool.xaml
    /// </summary>
    public partial class ExampleTool : UserControl
    {
        public ImageSource BackgroundImageBase
        {
            get { return (ImageSource)GetValue(BackgroundImageBaseProperty); }
            set { SetValue(BackgroundImageBaseProperty, value); }
        }
        public static readonly DependencyProperty BackgroundImageBaseProperty =
            DependencyProperty.Register("BackgroundImageBase", typeof(object), typeof(ExampleTool), new PropertyMetadata(""));

        public ImageSource BackgroundImageHover
        {
            get { return (ImageSource)GetValue(BackgroundImageHoverProperty); }
            set { SetValue(BackgroundImageHoverProperty, value); }
        }
        public static readonly DependencyProperty BackgroundImageHoverProperty =
            DependencyProperty.Register("BackgroundImageHover", typeof(object), typeof(ExampleTool), new PropertyMetadata(""));

        public ExampleTool()
        {
            InitializeComponent();
            DataContext = this;
            

        }
    }
}
