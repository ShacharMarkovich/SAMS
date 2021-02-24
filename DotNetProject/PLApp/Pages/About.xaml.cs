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
using System.Diagnostics;

namespace PLApp.Pages
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        public String Title { get; set; }
        public About()
        {
            Title = "About";
            InitializeComponent();
        }

        private void GoogleDrive_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://drive.google.com/drive/u/2/folders/1dffosvvP2Vk5JD3TvOfzXBt0J63SO9Il");
        }

        private void Github_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/ShacharMarkovich/SAMS");
        }

        private void Paypal_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://paypal.me/LevSAMS?locale.x=he_IL");
        }
    }
}
