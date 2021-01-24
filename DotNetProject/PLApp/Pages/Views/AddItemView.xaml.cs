using PLApp.Pages.ViewModels;
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
using System.Windows.Shapes;

namespace PLApp.Pages.Views
{
    /// <summary>
    /// Interaction logic for AddItemView.xaml
    /// </summary>
    public partial class AddItemView : Window
    {
        public AdditemViewModel VM;
        public AddItemView()
        {
            InitializeComponent();
            VM = new AdditemViewModel();
            this.DataContext = VM;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource itemViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("itemViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // itemViewSource.Source = [generic data source]
        }
    }
}
