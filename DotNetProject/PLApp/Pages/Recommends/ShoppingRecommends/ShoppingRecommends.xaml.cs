using System.Windows;
using System.Windows.Controls;
using PLApp.Pages.Recommends.ShoppingRecommends;
using System.Windows.Threading;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace PLApp.Pages
{
    /// <summary>
    /// Interaction logic for ShoppingRecommends.xaml
    /// </summary>
    public partial class ShoppingRecommends : UserControl
    {
        public string Title { get; set; }
        public ShoppingRecommendsViewModel M;
        public ShoppingRecommends()
        {
            Title = "Shopping Recommends";
            InitializeComponent();
            Task.Factory.StartNew(() => LoadDataInThread());
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
        private void LoadDataInThread()
        {

            M = new ShoppingRecommendsViewModel();
            Thread.Sleep(5000);
            Dispatcher.BeginInvoke(new Action(() =>
            {
                this.DataContext = M;
                LoadingPanel.Visibility = Visibility.Collapsed;
                DataGrid.Visibility = Visibility.Visible;
            }), DispatcherPriority.Background);
        }
    }
}
