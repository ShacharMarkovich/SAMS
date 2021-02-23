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

namespace PLApp.Pages.Catalog
{
    /// <summary>
    /// Interaction logic for Catalog.xaml
    /// </summary>
    public partial class Catalog : UserControl
    {
        public System.Collections.ObjectModel.ObservableCollection<BE.Item> CatalogItemList { get; set; }
        public Catalog()
        {
            CatalogItemList = new System.Collections.ObjectModel.ObservableCollection<BE.Item>
            {
                new BE.Item(){ItemName="מוצר1"},
                new BE.Item(){ItemName="מוצר2"},
                new BE.Item(){ItemName="מוצר2"},
                new BE.Item(){ItemName="מוצר2"},
                new BE.Item(){ItemName="מוצר2"},
                new BE.Item(){ItemName="מוצר2"}

            };
            InitializeComponent();
            itemsCardView.ItemsSource = CatalogItemList;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void itemsCardView_Initialized(object sender, EventArgs e)
        {
           (sender as MahApps.Metro.Controls.FlipView).HideControlButtons();
        }

        private void flipview_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((sender as MahApps.Metro.Controls.FlipView).SelectedIndex == 0)

                (sender as MahApps.Metro.Controls.FlipView).GoForward();
            else
                (sender as MahApps.Metro.Controls.FlipView).GoBack();
            (sender as MahApps.Metro.Controls.FlipView).HideControlButtons();


        }
    }
}
