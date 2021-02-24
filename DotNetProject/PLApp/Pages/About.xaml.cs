using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace PLApp.Pages
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
        }

        private void GoogleDrive_Click(object sender, RoutedEventArgs e) => Process.Start("https://drive.google.com/drive/u/2/folders/1dffosvvP2Vk5JD3TvOfzXBt0J63SO9Il");

        private void Github_Click(object sender, RoutedEventArgs e) => Process.Start("https://github.com/ShacharMarkovich/SAMS");

        private void Paypal_Click(object sender, RoutedEventArgs e) => Process.Start("https://paypal.me/LevSAMS?locale.x=he_IL");
    }
}
