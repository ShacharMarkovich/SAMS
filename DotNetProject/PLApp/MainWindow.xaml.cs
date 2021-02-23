using MahApps.Metro.Controls;
using PLApp.Pages;
using PLApp.Pages.Catalog;
using PLApp.Pages.Views;
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
    public partial class MainWindow : MetroWindow
    {
        private OrderView FillDataUC;
        private ShoppingAnalysis ShoppingAnalysisUC;
        private ShoppingRecommends ShoppingRecommendsUC;
        private About AboutUC;
        private Catalog CatalogUC;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_FillData_Click(object sender, RoutedEventArgs e)
        {
            OpenCloseDrawer();
            if (!(MainUserControl.Content is OrderView))
            {
                FillDataUC = new OrderView();
            }
            MainUserControl.Tag = FillDataUC.Tag;
            MainUserControl.Content = FillDataUC;
        }
        private void Button_Catalog_Click(object sender, RoutedEventArgs e)
        {
            OpenCloseDrawer();
            if (!(MainUserControl.Content is Catalog))
            {
                CatalogUC = new Catalog();
            }
            MainUserControl.Tag = CatalogUC.Tag;
            MainUserControl.Content = CatalogUC;
        }
        private void Button_ShoppingAnalysis_Click(object sender, RoutedEventArgs e)
        {
            OpenCloseDrawer();
            if (!(MainUserControl.Content is ShoppingAnalysis))
            {
                ShoppingAnalysisUC = new ShoppingAnalysis();
            }
            MainUserControl.Tag = ShoppingAnalysisUC.Tag;
            MainUserControl.Content = ShoppingAnalysisUC;

        }
        private void Button_ShoppingRecommends_Click(object sender, RoutedEventArgs e)
        {
            OpenCloseDrawer();
            if (!(MainUserControl.Content is ShoppingRecommends))
            {
                ShoppingRecommendsUC = new ShoppingRecommends();
            }
            MainUserControl.Tag = ShoppingRecommendsUC.Tag;
            MainUserControl.Content = ShoppingRecommendsUC;

        }
        private void Button_About_Click(object sender, RoutedEventArgs e)
        {
            OpenCloseDrawer();
            if (!(MainUserControl.Content is About))
            {
                AboutUC = new About();
            }
            MainUserControl.Tag = AboutUC.Tag;
            MainUserControl.Content = AboutUC;
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainFlayout.IsOpen = true;
        }

        private void MainFlayout_IsOpenChanged(object sender, RoutedEventArgs e)
        {
            if (MainFlayout.IsOpen)
                MainStackPanel.MouseLeftButtonUp += new MouseButtonEventHandler(SetMainFlayoutIsOpenToFalse);
            else
                MainStackPanel.MouseLeftButtonUp -= SetMainFlayoutIsOpenToFalse;
        }
        private void SetMainFlayoutIsOpenToFalse(object s, EventArgs e)
        {
            MainFlayout.IsOpen = false;
        }
        public void OpenCloseDrawer()
        {
            if (MainFlayout.IsOpen)
                MainFlayout.IsOpen = false;
            else
                MainFlayout.IsOpen = true;
        }
    }
}