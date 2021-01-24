using PLApp.Pages.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLApp.Pages.ViewModels
{
    public class AdditemViewModel
    {
        private AddItemModel additemM;
        public ObservableCollection<BE.Item> DriveItems { get; set; }
        public AdditemViewModel()
        {
            additemM = new AddItemModel();
            DriveItems = new ObservableCollection<BE.Item>(additemM.Items);
        }
    }
}
