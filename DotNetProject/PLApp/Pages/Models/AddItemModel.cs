using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLApp.Pages.Models
{
    class AddItemModel : INotifyPropertyChanged
    {
        public List<BE.Item> Items;


        public AddItemModel()
        {
            //TODO: load DRIVE qrcodes, parse
            Items = new List<BE.Item>() {
                new BE.Item(){ BarcodeNumber=1,ItemName="1"},
                new BE.Item(){BarcodeNumber=2,ItemName="2"},
                new BE.Item(){BarcodeNumber=3,ItemName="3"},
                new BE.Item(){BarcodeNumber=4,ItemName="4"}};
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
