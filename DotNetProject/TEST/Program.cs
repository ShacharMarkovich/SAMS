using System;
using System.Threading;
using DAL;

namespace DriveQuickstart
{
    class Program
    {
        public static void Main()
        {
            //DAL.GoogleDriveAPI.GetAllData();
            BL.DataHandle.parseBitmapList(BL.DataHandle.loadQRBitmaps());
            Console.ReadLine();
        }
    }
}
