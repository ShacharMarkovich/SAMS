using BE;
using DAL;

using System;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Item i = new Item("מוצר", "חברה", "150גרם", "ישראל", 2, 50);
            Order o = new Order(1,DateTime.Now);
            o.AddItem(i);
            DataBase db = new DataBase();
            db.OrderInsert(o);
            Console.WriteLine("");
        }
    }
}
