using System;
using System.Threading;
using DAL;
using TEST;

namespace DriveQuickstart
{
    class Program
    {
        public static void Main()
        {
            //DAL.GoogleDriveAPI.GetAllData();
            //BL.DataHandle.GenerateQRcodes();
            // BL.DataHandle.parseBitmapList(BL.DataHandle.loadQRBitmaps());
            FlushData.FlushAll();
            Console.WriteLine("press key to exit");
            Console.ReadLine();
        }
    }
}
