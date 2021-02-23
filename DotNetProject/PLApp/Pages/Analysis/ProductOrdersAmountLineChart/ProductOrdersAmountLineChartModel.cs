using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace PLApp.Pages.Analysis.ProductOrdersAmountLineChart
{
    internal class ProductOrdersAmountLineChartModel
    {
        public ICollection<Item> Items { get; private set; }
        public ICollection<Order> Orders { get; private set; }

        public ProductOrdersAmountLineChartModel()
        {
            Items = App.db.GetAllItems();
            Orders = App.db.GetOrders();
        }
    }
}
