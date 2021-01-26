using PLApp.Pages.ViewModels;
using System.Windows;
using System.Windows.Data;

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
            DataContext = VM;
            InsetItemGrid.DataContext = VM.itemViewSource;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            CollectionViewSource itemViewSource = ((CollectionViewSource)(FindResource("itemViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // itemViewSource.Source = [generic data source]
        }
    }
}
