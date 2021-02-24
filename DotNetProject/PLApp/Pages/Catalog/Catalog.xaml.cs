using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PLApp.Pages.Catalog
{
    /// <summary>
    /// Interaction logic for Catalog.xaml
    /// </summary>
    public partial class Catalog : UserControl
    {
        BL.DataHandle db;
        public List<BE.Item> CatalogItemList { get; set; }
        public Catalog()
        {
            db = new BL.DataHandle();
            CatalogItemList = db.GetAllItemsRemoveDuplicates();
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
