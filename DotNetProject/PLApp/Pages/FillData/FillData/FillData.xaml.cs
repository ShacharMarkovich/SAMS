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

namespace PLApp.Pages
{
    /// <summary>
    /// Interaction logic for FillData.xaml
    /// </summary>
    public partial class FillData : UserControl
    {
        public String Title { get; set; }

        public FillData()
        {
            Title = "Fill Data";
            //App.MainWindow.Title = "Fill Data";

            InitializeComponent();
        }
    }
}
