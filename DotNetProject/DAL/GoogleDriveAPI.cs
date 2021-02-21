using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing;
using Newtonsoft.Json.Linq;
using BE;
using System.Reflection;
using Newtonsoft.Json;

namespace DAL
{

    /// <summary>
    /// Google Drive Connection class.
    /// </summary>

    public static class GoogleDriveAPI
    {
        const string QRCodesDirectoryID = "1dffosvvP2Vk5JD3TvOfzXBt0J63SO9Il";
        public static readonly string saveDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\QRCodes\";
        /// <summary>
        /// Path to the client secret json file from Google Developers console.
        /// </summary>
        const string credentials = @"..\..\..\DAL\credentials.json";

        /// <summary>
        /// Download QR Codes from google drive, tgan delete them from google drive
        /// The QR codes images will save at @projectDirectory\QRCodes
        /// </summary>
        public static void DownloadGoogleDriveAPI()
        {
            DriveService service = AuthenticateOauth();
            IList<Google.Apis.Drive.v3.Data.File> files = GetDriveData(service);
            if (files != null)
                foreach (var file in files)
                {
                    DownloadFromDrive(service, file);
                    DeleteFileFromGoogleDrive(service, file);
                }
        }

        /// <summary>
        /// This method requests Authentcation from a user using Oauth2.  
        /// </summary>
        /// <returns>DriveService used to make requests against the Drive API</returns>
        private static DriveService AuthenticateOauth()
        {
            try
            {
                if (string.IsNullOrEmpty(credentials))
                    throw new ArgumentNullException("credentials");
                if (!File.Exists(credentials))
                    throw new Exception("credentials json file does not exist.");

                // These are the scopes of permissions you need. It is best to request only what you need and not all of them
                string[] scopes = new string[] { DriveService.Scope.Drive}; // View the files in your Google Drive
                UserCredential credential;
                using (var stream = new FileStream(credentials, FileMode.Open, FileAccess.Read))
                {
                    string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    credPath = Path.Combine(credPath, ".credentials/", Assembly.GetExecutingAssembly().GetName().Name);

                    // Requesting Authentication or loading previously stored authentication for userName
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, scopes, "user",
                                                                             CancellationToken.None, new FileDataStore(credPath, true)).Result;
                }

                // Create Drive API service.
                return new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Drive Oauth2 Authentication Sample"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create Oauth2 account DriveService failed" + ex.Message);
                throw new Exception("CreateServiceAccountDriveFailed", ex);
            }
        }

        /// <summary>
        /// Download all QR codes from the `QRCodes` directory in Google Drive
        /// </summary>
        /// <param name="service"> Google drive API service</param>
        /// <returns>list of the files in the `QRCodes` directory</returns>
        private static IList<Google.Apis.Drive.v3.Data.File> GetDriveData(DriveService service)
        {
            // Define parameters of request:
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 100;
            listRequest.Fields = "nextPageToken, files(id, name, parents, modifiedTime)";
            listRequest.Q = $"'{QRCodesDirectoryID}' in parents";

            // List files:
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
            if (files == null || files.Count == 0)
                return null;
            return files;
        }

        /// <summary>
        /// Download a given file from google drive
        /// </summary>
        /// <param name="service"> Google drive API service</param>
        /// <param name="file">file to download from google drive</param>
        private static void DownloadFromDrive(DriveService service, Google.Apis.Drive.v3.Data.File file)
        {
            using (FileStream fileStream = new FileStream(saveDirectory + file.Name, FileMode.OpenOrCreate))
                service.Files.Get(file.Id).Download(fileStream);
        }

        /// <summary>
        /// Permanently delete the given file from google drive.
        /// </summary>
        /// <param name="service"> Google drive API service</param>
        /// <param name="file">file to download from google drive</param>
        private static void DeleteFileFromGoogleDrive(DriveService service, Google.Apis.Drive.v3.Data.File file)
        {
            service.Files.Delete(file.Id).Execute();
        }
    }
}
