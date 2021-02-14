using System;
using System.Drawing;
using System.IO;

namespace DriveQuickstart
{
    class Program
    {
        public static void Main()
        {
            //DAL.GoogleDriveAPI.DownloadGoogleDriveAPI();
            DAL.GoogleDriveAPI.GetAllData();
            Console.ReadLine();
        }
    }
}
