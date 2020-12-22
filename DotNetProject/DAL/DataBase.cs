using BE;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace DAL
{
    public class DataBase
    {
        private string databasePath;
        private SQLiteConnection db;
        public DataBase()
        {
            databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");
            db = new SQLiteConnection(databasePath);
            db.CreateTable<Item>();
            db.CreateTable<Order>();
        }
        public void OrderInsert(Order o)
        {
            try
            {
                db.Insert(o);
                Console.WriteLine("Success");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
