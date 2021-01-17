using PLApp.Pages;
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

namespace PLApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_FillData_Click(object sender, RoutedEventArgs e)
        {
            if (!(MainUserControl.Content is FillData))
            {
                MainUserControl.Content = new FillData();
            }
        }
        private void Button_ShoppingAnalysis_Click(object sender, RoutedEventArgs e)
        {
            if (!(MainUserControl.Content is ShoppingAnalysis))
            {
                MainUserControl.Content = new ShoppingAnalysis();
            }
        }
        private void Button_ShoppingRecommends_Click(object sender, RoutedEventArgs e)
        {
            if (!(MainUserControl.Content is ShoppingRecommends))
            {
                MainUserControl.Content = new ShoppingRecommends();
            }
        }
        private void Button_Settings_Click(object sender, RoutedEventArgs e)
        {
            if (!(MainUserControl.Content is Settings))
            {
                MainUserControl.Content = new Settings();
            }
        }
        private void Button_About_Click(object sender, RoutedEventArgs e)
        {
            if (!(MainUserControl.Content is About))
            {
                MainUserControl.Content = new About();
            }
        }
    }
}