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

namespace DAL
{

    /// <summary>
    /// Google Drive Connection class.
    /// </summary>

    public class GoogleDriveAPI
    {
        const string QRCodesDirectoryID = "1dffosvvP2Vk5JD3TvOfzXBt0J63SO9Il";
        const string saveDirectory = @"..\..\..\DAL\QRCodes\";
        /// <summary>
        /// Path to the client secret json file from Google Developers console.
        /// </summary>
        const string credentials = @"..\..\..\DAL\credentials.json";

        #region Convert data in QR Code to Object
        static Item Convert(string data)
        {
            JObject jObject = JObject.Parse(data); /* {"BarcodeNumber": 123447, "ItemName": "56' TV", "StoreName": "Tiv TaAm", "ItemPrice": 1222.9 } */
            return new Item() { 
                BarcodeNumber = (int)jObject["BarcodeNumber"], 
                ItemName = (string)jObject["ItemName"], 
                ItemPrice = (int)jObject["ItemPrice"]
            };
        }
        #endregion

        #region Decode data from QR Code
        public static void DecodeQRCode(string imageFileName)
        {
            QRCodeDecoder decoder = new QRCodeDecoder();
            FileStream file = new FileStream(imageFileName, FileMode.OpenOrCreate);
            QRCodeBitmapImage qrCodeBitmapImage = new QRCodeBitmapImage(new Bitmap(file));
            string data = decoder.Decode(qrCodeBitmapImage);
            Convert(data);
        }

        public static void GetAllData()
        {
            string[] names = Directory.GetFiles(saveDirectory);
            foreach (string name in names)
                DecodeQRCode(name);
        }
        #endregion

        #region download QR Codde images from google drive functions
        /// <summary>
        /// This method requests Authentcation from a user using Oauth2.  
        /// </summary>
        /// <returns>DriveService used to make requests against the Drive API</returns>
        static DriveService AuthenticateOauth()
        {
            try
            {
                if (string.IsNullOrEmpty(credentials))
                    throw new ArgumentNullException("credentials");
                if (!File.Exists(credentials))
                    throw new Exception("credentials json file does not exist.");

                // These are the scopes of permissions you need. It is best to request only what you need and not all of them
                string[] scopes = new string[] { DriveService.Scope.DriveReadonly };         	//View the files in your Google Drive                                                 
                UserCredential credential;
                using (var stream = new FileStream(credentials, FileMode.Open, FileAccess.Read))
                {
                    string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    credPath = Path.Combine(credPath, ".credentials/", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);

                    // Requesting Authentication or loading previously stored authentication for userName
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, scopes, "user name",
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
        /// Download QR Codes from google drive.
        /// The QR codes images will save at @projectDirectory\QRCodes
        /// </summary>
        public static void DownloadGoogleDriveAPI()
        {
            // TODO: download only new QR codes that not in the project directory
            DriveService service = AuthenticateOauth();
            IList<Google.Apis.Drive.v3.Data.File> files = GetDriveData(service);
            if (files != null)
                foreach (var file in files)
                    DownloadFileAsync(service, file, saveDirectory + file.Name);
        }
        /// <summary>
        /// Download all QR codes from the `QRCodes` directory in Google Drive
        /// </summary>
        /// <param name="service"> Google drive API service</param>
        /// <returns>list of the files in the `QRCodes` directory</returns>
        static IList<Google.Apis.Drive.v3.Data.File> GetDriveData(DriveService service)
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
        /// download the given file, save it in saveTo directory
        /// </summary>
        /// <param name="service">Google drive API service</param>
        /// <param name="file">google drive api file</param>
        /// <param name="saveTo">directory to save the files</param>
        static async System.Threading.Tasks.Task DownloadFileAsync(DriveService service, Google.Apis.Drive.v3.Data.File file, string saveTo)
        {

            var request = service.Files.Get(file.Id);
            var stream = new MemoryStream();

            // Add a handler which will be notified on progress changes.
            // It will notify on each chunk download and when the
            // download is completed or failed.
            request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
            {
                switch (progress.Status)
                {
                    case Google.Apis.Download.DownloadStatus.Downloading:
                        Console.WriteLine(progress.BytesDownloaded);
                        break;
                    case Google.Apis.Download.DownloadStatus.Completed:
                        Console.WriteLine("Download complete.");
                        Thread.Sleep(1000);
                        SaveStream(stream, saveTo);
                        break;
                    case Google.Apis.Download.DownloadStatus.Failed:
                        Console.WriteLine("Download failed.");
                        break;
                }
            };
            request.Download(stream);
        }
        /// <summary>
        /// save the MemoryStream to the given directory
        /// </summary>
        /// <param name="stream">the stream</param>
        /// <param name="saveTo">directory to save the files</param>
        static void SaveStream(MemoryStream stream, string saveTo) => stream.WriteTo(new FileStream(saveTo, FileMode.Create, FileAccess.Write));
        #endregion 
    }
}
