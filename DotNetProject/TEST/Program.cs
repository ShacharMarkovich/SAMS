using System;
using System.Threading;
using DAL;

namespace DriveQuickstart
{
    class Program
    {
        public static void Main()
        {
            GoogleDriveAPI.DownloadGoogleDriveAPI();
            Thread.Sleep(1000);
            GoogleDriveAPI.GetAllData();
            Console.ReadLine();
        }
    }
}
