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
    /// Interaction logic for ImageSwapButton_custom.xaml
    /// </summary>
    public partial class ImageSwapButton_custom : UserControl
    {
        public ImageSource OnHoverBg
        {
            get { return (ImageSource)GetValue(OnHoverBgProperty); }
            set { SetValue(OnHoverBgProperty, value); }
        }
        public static readonly DependencyProperty OnHoverBgProperty =
            DependencyProperty.Register("OnHoverBg", typeof(object), typeof(ImageSwapButton_custom));


        public ImageSource OnClickBg
        {
            get { return (ImageSource)GetValue(OnClickBgProperty); }
            set { SetValue(OnClickBgProperty, value); }
        }
        public static readonly DependencyProperty OnClickBgProperty =
            DependencyProperty.Register("OnClickBg", typeof(object), typeof(ImageSwapButton_custom));

         public ImageSource BaseBg
        {
            get { return (ImageSource)GetValue(BaseBgProperty); }
            set { SetValue(BaseBgProperty, value); }
        }
        public static readonly DependencyProperty BaseBgProperty =
            DependencyProperty.Register("BaseBg", typeof(object), typeof(ImageSwapButton_custom));

        public ICommand CustomImageCommand
        {
            get { return (ICommand)GetValue(CustomImageCommandProperty); }
            set { SetValue(CustomImageCommandProperty, value); }
        }
        public static readonly DependencyProperty CustomImageCommandProperty =
            DependencyProperty.Register("CustomImageCommand", typeof(ICommand), typeof(ImageSwapButton_custom));

        public ImageSwapButton_custom()
        {
            InitializeComponent();
        }
    }
}
