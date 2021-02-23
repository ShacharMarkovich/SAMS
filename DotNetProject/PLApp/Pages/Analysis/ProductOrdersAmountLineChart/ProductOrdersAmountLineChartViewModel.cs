using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using BE;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Geared;

namespace PLApp.Pages.Analysis.ProductOrdersAmountLineChart
{
    internal class ProductOrdersAmountLineChartViewModel : INotifyPropertyChanged
    {
        #region Implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        #region Geared values
        public object Mapper { get; set; }
        public GearedValues<DateTimePoint> Values { get; set; }

        private Func<double, string> _formatter;
        public Func<double, string> Formatter
        {
            get { return _formatter; }
            set
            {
                _formatter = value;
                OnPropertyChanged("Formatter");
            }
        }

        private double _from;
        public double From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged("From");
            }
        }

        private double _to;
        public double To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged("To");
            }
        }
        #endregion

        private ProductOrdersAmountLineChartModel Model;
        public ObservableCollection<string> ItemsData { get; set; }

        public List<string> StoresName { get; set; }

        public ProductOrdersAmountLineChartViewModel()
        {
            Model = new ProductOrdersAmountLineChartModel();
            StoresName = App.db.GetStoresName();
            ItemsData = new ObservableCollection<string>();
            List<DateTimePoint> Values = new List<DateTimePoint>();
            Formatter = x => new DateTime((long)x).ToString("yyyy");
            From = DateTime.Now.AddHours(500000).Ticks;
            To = DateTime.Now.AddHours(500).Ticks;
        }

        /// <summary>
        /// Update the Items in the itemsComboBox to show only item which had been bought in the given store
        /// </summary>
        /// <param name="storeName"> the store name</param>
        /// <returns>IEnumerable of items data which had been bought in the given store</returns>
        internal ObservableCollection<string> UpdateItemsSource(string storeName)
        {
            if (Values == null)
                Values = new GearedValues<DateTimePoint>();
            Values.Clear();
            List<Item> itemsLst = App.db.GetAllItemInStore(storeName);
            ItemsData = new ObservableCollection<string>(itemsLst.Where(item => item.Quantity > 0).Select(item => $"{item.BarcodeNumber} {item.ItemName}").OrderBy(i => i).Distinct());
            return ItemsData;
        }


        /// <summary>
        /// Update the values in GearedValues list to show order time of selected item
        /// </summary>
        /// <param name="itemBarcode">selected item's barcode number </param>
        /// <param name="storeName">selected item's store name</param>
        private void UpdateValues(int itemBarcode, string storeName)
        {
            // get all orders in `storeName` store:
            DateTime first = DateTime.Now, last = DateTime.Now;
            List<Order> ordersInCurrStore = Model.Orders.Where(order => order.StoreName == storeName).ToList();
            List<DateTimePoint> storeValues = new List<DateTimePoint>();

            foreach (var order in ordersInCurrStore)
            {
                var lst = order.Items.Where(item => item.BarcodeNumber == itemBarcode)
                                     .Select(item => new DateTimePoint(order.OrderDate, (double)item.Quantity)).ToList();
                storeValues.AddRange(lst);

                if (order.OrderDate < first) first = order.OrderDate;
                if (order.OrderDate > last) last = order.OrderDate;
            }
            Values = storeValues.AsGearedValues().WithQuality(Quality.High);

            // update the shown values on axises
            From = first.Ticks;
            To = last.Ticks;
        }

        /// <summary>
        /// show the selected item orders amount in each store
        /// </summary>
        /// <param name="itemData"></param>
        /// <param name="storeName"></param>
        public void ItemCheckBoxSelectionChange(string itemData, string storeName)
        {
            Item selectedItem = Model.Items.Where(item => $"{item.BarcodeNumber} {item.ItemName}" == itemData).First();
            UpdateValues(selectedItem.BarcodeNumber, storeName);
        }
    }
}
