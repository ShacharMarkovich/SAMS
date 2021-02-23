using Accord.MachineLearning.Rules;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BE;

namespace PLApp.Pages
{
    /// <summary>
    /// Interaction logic for ShoppingRecommends.xaml
    /// </summary>
    public partial class ShoppingRecommends : UserControl
    {
        public string Title { get; set; }

        public ShoppingRecommends()
        {
            Title = "Shopping Recommends";
            InitializeComponent();
            itemListView.ItemsSource = App.db.GetAllItemsRemoveDuplicates().ToList().GetRange(0, 10);
        }

        public void foo()
        {

        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dlg = new PrintDialog();
            if ((bool)dlg.ShowDialog().GetValueOrDefault())
            {
                dlg.PrintTicket.PageMediaSize = new System.Printing.PageMediaSize(itemListView.ActualWidth, itemListView.ActualHeight);
                dlg.PrintVisual(itemListView, "Recommendations");
            }
        }
    }
}
