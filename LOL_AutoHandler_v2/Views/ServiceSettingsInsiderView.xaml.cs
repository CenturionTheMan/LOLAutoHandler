using LOL_AutoHandler_v2.Models;
using LOL_AutoHandler_v2.ViewModels;
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

namespace LOL_AutoHandler_v2.Views
{
    
    public partial class Service_TopView : UserControl
    {
        public Service_TopView(ServiceSettingsViewModel parent)
        {
            InitializeComponent();
            DataContext = parent;
        }
    }
}
