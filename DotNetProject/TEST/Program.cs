using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
namespace TEST
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            using (var ctx = new Database())
            {
                Item i = new Item(3,"Name", "manufacturer", "unitQty", "Country", 1, 10);
                ctx.Items.Add(i);
                ctx.SaveChanges();
                Console.WriteLine("Saved");
                Console.ReadKey();
            }
        }
    }
}
