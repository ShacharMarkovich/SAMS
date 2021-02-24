using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Wpf;
using PLApp.Pages.Analysis.AverageOrderCostBarChart;
using BE;
using System.Collections.ObjectModel;

namespace PLApp.Pages.Analysis.ProductOrdersAmountLineChart
{
    internal class ProductOrdersAmountLineChartViewModel : INotifyPropertyChanged
    {
        struct tmp
        {
            public DateTime orderTime;
            public List<Item> items;
        }

        #region Implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        #region LiveCharts class property
        string[] xlabel; // property that hold the X axis labels. Implements INotifyPropertyChanged
        public string[] Xlabel
        {
            get => xlabel;
            set
            {
                xlabel = value;
                OnPropertyChanged("xlabel");
            }
        }

        string xTitle; // property that hold the X axis labels. Implements INotifyPropertyChanged
        public string XTitle
        {
            get => xTitle;
            set
            {
                xTitle = value;
                OnPropertyChanged("xTitle");
            }
        }

        public SeriesCollection ProductsAmountCollection { get; set; } // hold the Stores Graph collection
        public Func<double, string> Formatter { get; set; } // the grapf Formatter
        #endregion

        public AverageOrderCostBarChartModel Model;

        /// <summary>
        ///  Encapsulates a function that get an Order and return orderDate's day-in-month/month/year, according to user choise.
        /// </summary>
        private Func<DateTime, string> xAxisVal;

        public ICommand StoreCheckBoxCommand { get; set; }

        public List<string> StoresName { get; set; }

        private ObservableCollection<string> _itemsData;
        public ObservableCollection<string> ItemsData
        {
            get => _itemsData;
            set
            {
                _itemsData = value;
                OnPropertyChanged("ItemsData");
            }
        }

        public ProductOrdersAmountLineChartViewModel()
        {
            Model = new AverageOrderCostBarChartModel();
            StoresName = Model.db.GetStoresName();

            ProductsAmountCollection = new SeriesCollection();
            Formatter = value => value.ToString("N");
        }

        /// <summary>
        /// Build the Shops Average Order Total Cost graph.
        /// thr Graph is depending on time that the user select (year/month/day)
        /// Show in the "Shops Statistics" tab
        /// </summary>
        /// <param name="yearStackPanelVisibility">selct year's StackPanel visibility</param>
        /// <param name="monthStackPanelVisibility">selct month's StackPanel visibility</param>
        public void BuildShopCart(Visibility yearStackPanelVisibility, Visibility monthStackPanelVisibility)
        {
            // now `orders` contains only orders that had been done in the selected store
            List<Order> orders = Model.Orders.Where(order => order.StoreName == Model.selectedStoreName).ToList();
            orders = SetLabalNTitle(yearStackPanelVisibility, monthStackPanelVisibility, orders);

            var itemsLst2 = Model.db.GetAllItemInStore(Model.selectedStoreName)
                                .Where(item => item.Quantity > 0 && item.BarcodeNumber == Model.selectedItem.BarcodeNumber)
                                .Select(item => item.Quantity).Sum();
            ProductsAmountCollection.Clear();
            // create a list of zerose. for each day of the week.
            // it is array of lists because we maybe have more than 1 order in same day
            List<double>[] XaxisVals = Enumerable.Repeat(0, Xlabel.Length).Select(_ => new List<double> { 0 }).ToArray();
            // for each order in that store, culculate the sums of items price that the user buy at a spesific order date
            List<tmp> itemsLst = orders.Select(order => new tmp() { orderTime = order.OrderDate, items = order.Items.ToList() }).ToList();

            foreach (var itemLst in itemsLst)
            {
                int selectedItemInOrdersAmonut = itemLst.items
                                                .Where(item => item.BarcodeNumber == Model.selectedItem.BarcodeNumber && item.Quantity > 0)
                                                .Select(item => (int)item.Quantity).Sum();
                //int selectedItemInOrdersAmonut = order.Items.Where(item => item == Model.selectedItem && item.Quantity > 0).Select(item => (int)item.Quantity).Sum();
                int i = Array.IndexOf(Xlabel, xAxisVal(itemLst.orderTime));
                if (XaxisVals[i][0] == 0)
                    XaxisVals[i][0] = selectedItemInOrdersAmonut;
                else
                    XaxisVals[i].Add(selectedItemInOrdersAmonut);
            }
            // calc the average cost in the store in specific day:
            var sumPrices = XaxisVals.Select(amountList => amountList.Sum()).ToList();
            var total = sumPrices.Sum();
            ProductsAmountCollection.Add(new ColumnSeries { Title = Model.selectedStoreName, Values = new ChartValues<double>(sumPrices) });
        }

        /// <summary>
        /// Set the Tile and the X axis label of the graph
        /// </summary>
        /// <param name="yearStackPanelVisibility">selct year's StackPanel visibility</param>
        /// <param name="monthStackPanelVisibility">selct month's StackPanel visibility</param>
        /// <param name="orders"> list of all orders</param>
        /// <returns></returns>
        private List<Order> SetLabalNTitle(Visibility yearStackPanelVisibility, Visibility monthStackPanelVisibility, List<Order> orders)
        {
            // check if user want to see the average order cost per year
            if (yearStackPanelVisibility == Visibility.Collapsed && monthStackPanelVisibility == Visibility.Collapsed)
            {
                xAxisVal = OrderDate => OrderDate.Year.ToString();
                Xlabel = Model.db.GetOrdersYear().Select(i => i.ToString()).ToArray();
                XTitle = "Year";
            }
            // check if user want to see the average order cost per months in given year
            else if (yearStackPanelVisibility == Visibility.Visible && monthStackPanelVisibility == Visibility.Collapsed)
            {
                xAxisVal = OrderDate => Model.months[OrderDate.Month - 1];
                orders = orders.Where(order => order.OrderDate.Year == Model.selectYear).ToList(); // get order in given year
                Xlabel = orders.Select(order => order.OrderDate.Month).Select(month => Model.months[month - 1]).Distinct().ToArray(); // get months of those orders
                XTitle = "Month";
            }
            else // user want to see the average order cost per day in given months and year
            {
                xAxisVal = OrderDate => (OrderDate.Day).ToString();
                orders = orders.Where(order => order.OrderDate.Month == Model.selectMonth && order.OrderDate.Year == Model.selectYear).ToList();
                Xlabel = Enumerable.Range(1, 31).Select(i => i.ToString()).ToArray(); // the X axis labales will update in the end...
                XTitle = "Day in Month";
            }

            return orders;
        }

        /// <summary>
        /// show the selected item orders amount in each store
        /// </summary>
        /// <param name="itemData"></param>
        /// <param name="storeName"></param>
        public void ItemCheckBoxSelectionChange(string itemData, string storeName)
        {
            Item selectedItem = Model.Items.Where(item => $"{item.BarcodeNumber} {item.ItemName}" == itemData).First(); // get the selected item
            Model.selectedItem = selectedItem;
        }
        /// <summary>
        /// Update the Items in the itemsComboBox to show only item which had been bought in the given store
        /// </summary>
        /// <param name="storeName"> the store name</param>
        /// <returns>IEnumerable of items data which had been bought in the given store</returns>
        internal ObservableCollection<string> UpdateItemsSource(string storeName)
        {
            ProductsAmountCollection.Clear();
            List<Item> itemsLst = Model.db.GetAllItemInStore(storeName);
            ItemsData = new ObservableCollection<string>(itemsLst.Where(item => item.Quantity > 0).Select(item => $"{item.BarcodeNumber} {item.ItemName}").OrderBy(i => i).Distinct());
            return ItemsData;
        }
    }
}
