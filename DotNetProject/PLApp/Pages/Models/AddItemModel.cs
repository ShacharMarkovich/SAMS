using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLApp.Pages.Models
{
    class AddItemModel
    {
        public List<BE.Item> Items;

        public AddItemModel()
        {
            //TODO: load DRIVE qrcodes, parse
            Items = new List<BE.Item>();
        }
    }
}
