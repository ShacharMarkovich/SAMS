using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            Apriori<Item> ab = new Apriori<Item>(24, 0);
            var clsfr = ab.Learn(a);
            var itemsDistinctByBarcode = db.GetAllItems().GroupBy(i => i.BarcodeNumber).Select(grp => grp.FirstOrDefault());
            AssociationRuleMatcher<Item> cl = new AssociationRuleMatcher<Item>(itemsDistinctByBarcode.Count(),
                clsfr.Rules.Where(r => r.Confidence > 0.6).ToArray());

            int abcde;
            System.Collections.Generic.Dictionary<Item, int> counter = new System.Collections.Generic.Dictionary<Item, int>();
            foreach (Order o in db.GetOrders()){
                foreach (Item item in o.Items)
                {
                    if (counter.TryGetValue(item,out abcde))
                    {
                        
                    }
                }
            }
            var abcd = cl.Decide(itemsDistinctByBarcode.ToList().GetRange(0,10).ToArray());
            AssociationRule<Item>[] rules = clsfr.Rules;
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
