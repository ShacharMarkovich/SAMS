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
            Items = new List<BE.Item>();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
