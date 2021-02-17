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
            Console.ReadLine();
        }
    }
}
