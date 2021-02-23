using System;
using System.Linq;
using System.Threading;
using Accord.MachineLearning.Rules;
using BE;
using DAL;
using TEST;

namespace DriveQuickstart
{
    class Program
    {
        public static void Main()
        {
            BL.DataHandle db = BL.DataHandle.Instance;
            Item[][] a = db.GetOrders().Select(order => order.Items.ToArray()).ToArray();
            Apriori<Item> ab = new Apriori<Item>(0, 0);
            AssociationRule<Item>[] rules = ab.Learn(a).Rules;
            foreach (var rule in rules)
                Console.WriteLine(rule);
            Console.ReadLine();
            //BL.DataHandle.GenerateQRcodes();
            //FlushData.FlushAll();
            //Console.WriteLine("press key to exit");
            //Console.ReadLine();
        }
    }
}
