using PLApp.Pages.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Input;

namespace PLApp.Pages.ViewModels
{
    public class AdditemViewModel
    {
        private AddItemModel additemM;
        public Button AddButton;
        public BE.Item itemViewSource;
        public ObservableCollection<BE.Item> DriveItems { get; set; }
        public readonly int OrderId;
        public AdditemViewModel(int orderId)
        {
            OrderId = orderId;
            itemViewSource = new BE.Item() { BarcodeNumber = 123465};
            additemM = new AddItemModel();
            DriveItems = new ObservableCollection<BE.Item>(additemM.Items);
            AddButton = new Button();
            AddButtonCommand  = new RelayCommand(new Action<object>(AddButton_Click));
        }

        public ICommand AddButtonCommand { get; set; }


        private void AddButton_Click(object sender)
        {
            App.db.AddItemToOrderById(OrderId, itemViewSource);
            MessageBox.Show(itemViewSource.ItemName + "Add success");
        }
    }
}
