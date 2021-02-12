using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DriveQuickstart
{
    class Program
    {
        public static void Main() => DAL.GoogleDriveAPI.DownloadGoogleDriveAPI();
    }
}
