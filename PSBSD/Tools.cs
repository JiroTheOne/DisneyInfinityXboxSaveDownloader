using Newtonsoft.Json;
using System.Reflection;
using XboxCsMgr.Helpers.Win32;
using XboxCsMgr.XboxLive;
using XboxCsMgr.XboxLive.Exceptions;
using XboxCsMgr.XboxLive.Model.Authentication;
using XboxCsMgr.XboxLive.Model.TitleStorage;
using XboxCsMgr.XboxLive.Services;



namespace PSBSD
{
    internal class Config
    {
        internal static readonly string ServiceId2 = "99330100-8230-4304-8448-b7b80ffcf70b";
        internal static readonly string PackageFamilyName2 = "DisneyConsumerProductsand.DisneyInfinityRevolution_jrvsvx228t8wp";

        internal static readonly string ServiceId3 = "7dd70100-ef4f-47b7-bcfb-0e6a09230e16";
        internal static readonly string PackageFamilyName3 = "DisneyConsumerProductsand.Infinity3_jrvsvx228t8wp";
        internal static string UserToken { get; set; }
        internal static string DeviceToken { get; set; }
        public static readonly string FinalMessage = "Thank you for using our app.\n\nFor the sake of preservation please consider sharing/donating your Toyboxes with us on the Disney Infinity discord\nWe are trying to launch a preservation and archival project.\nClick on the discord icon for a invite.";

        internal static readonly DateTime LaunchDatetime = DateTime.Now;
        internal static readonly string Disclaimer = "DISCLAIMER:\n* I understand that this software connects to Xbox live REST API and beside that no data is collected or shared.\n* I understand that the credentials are taken from the Xbox app and login details are not necessary\n* I understand that running this program multiple times in an hour has the chance to get me rate limited and potentially banned.\n* I understand that developer is not responsible for my misuse";
        internal static string OutputPath = "";
        internal static string ServiceId = ServiceId2;
        internal static string PackageFamilyName = PackageFamilyName2;
        internal static readonly string MetaFileName = ".partial.disneyinfinity";
    }
    internal class Tools
    {
        internal static XboxLiveConfig XblConfig { get; set; }
        private static TitleStorageService _storageService;
        private static AuthenticateService authenticateService = new(XblConfig);

        public static void SelectFolder()
        {
            FolderBrowserDialog dialog = new();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {

                if (Directory.EnumerateFileSystemEntries(dialog.SelectedPath).Any())
                {
                    if (File.Exists(Path.Combine(dialog.SelectedPath, Config.MetaFileName)))
                    {
                        Log("partial download detected. Folder selected");
                    }
                    else
                    {
                        _ = MessageBox.Show("Please select a Empty folder", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SelectFolder();
                        return;
                    }
                }
                Config.OutputPath = dialog.SelectedPath;
                Tools.Log($"Output Folder selected: {dialog.SelectedPath}");
            }
            else
            {
                return;
            }
        }

        public static void Error(Exception ex)
        {
            Log($"Error:{ex.GetType()}\n\n{ex.Message}\n\n{ex.StackTrace}");
            _ = MessageBox.Show($"Error:{ex.GetType()}\n\n{ex.Message}\n\n{ex.StackTrace}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            /*Log("Exiting");
            Environment.Exit(1);*/
        }
        public static void Error(string s)
        {
            Log($"Error:{s}");
            _ = MessageBox.Show($"Error:{s}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void Log(string s)
        {
            using (FileStream output = new(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + Config.LaunchDatetime.ToFileTime() + ".log", FileMode.Append))
            using (StreamWriter sw = new(output))
            {
                sw.Write($"({DateTime.Now.ToLongTimeString()}) - ");
                sw.WriteLine(s);
            }
            Downloader.main.Log(s);
        }
        internal static void LoadXblTokenCredentials()
        {
            Dictionary<string, string> currentCredentials = CredentialUtil.EnumerateCredentials();
            Dictionary<string, string> xblCredentials = currentCredentials.Where(k => k.Key.Contains("Xbl|") || (k.Key.Contains("XblGrts|")
                    && k.Key.Contains("Dtoken"))
                    || k.Key.Contains("Utoken"))
                    .ToDictionary(p => p.Key, p => p.Value);

            string PartialCredential = null;
            foreach (KeyValuePair<string, string> credential in xblCredentials)
            {
                string json = credential.Value;
                XboxLiveToken token = null;
                try
                {
                    token = JsonConvert.DeserializeObject<XboxLiveToken>(json);
                }
                catch (JsonReaderException)
                {

                    if (PartialCredential == null)
                    {
                        PartialCredential = json;
                    }
                    else
                    {
                        try
                        {
                            token = JsonConvert.DeserializeObject<XboxLiveToken>(json + PartialCredential);
                        }
                        catch (JsonReaderException)
                        {
                            try
                            {
                                token = JsonConvert.DeserializeObject<XboxLiveToken>(PartialCredential + json);
                            }
                            catch (JsonReaderException)
                            {
                                PartialCredential = json;
                                break;
                            }
                        }
                    }
                }
                if (token != null)
                {
                    if (token.TokenData.NotAfter > DateTime.UtcNow)
                    {
                        if (credential.Key.Contains("Dtoken"))
                        {
                            Config.DeviceToken = token.TokenData.Token;
                        }
                        else if (credential.Key.Contains("Utoken"))
                        {
                            if (token.TokenData.Token != "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")
                            {
                                Config.UserToken = token.TokenData.Token;
                            }
                        }
                    }
                }
            }
        }
        internal static async Task AuthenticateXbl()
        {
            XboxLiveAuthenticateResponse<XboxLiveDisplayClaims> result;

            try
            {
                result = await authenticateService.AuthorizeXsts(Config.UserToken, Config.DeviceToken);
            }
            catch (Exception ex)
            {
                Error(ex);
                return;
            }
            if (result != null)
            {
                Log($"Authorized as :{result.DisplayClaims.XboxUserIdentity.First().Gamertag}");
                XblConfig = new XboxLiveConfig(result.Token, result.DisplayClaims.XboxUserIdentity[0]);
                authenticateService = new AuthenticateService(XblConfig);
                _storageService = new TitleStorageService(XblConfig, Config.PackageFamilyName, Config.ServiceId);
            }
            return;
        }
        internal static async Task<Byte[]> DownloadAtomData(string atom)
        {
            return await _storageService.DownloadAtomAsync(atom);
        }
        internal static async Task Download()
        {
            bool noerror = true;
            bool partial = false;
            Downloader.main.ProgressBarMarquee();
            Downloader.main.Disableinput();
            if (Directory.EnumerateFileSystemEntries(Config.OutputPath).Any())
            {
                if (File.Exists(Path.Combine(Config.OutputPath, Config.MetaFileName)))
                {
                    partial = true;
                }
                else
                {
                    Error(new Exception("FOLDER CHOSEN IS NOT EMPTY"));
                    noerror = false;
                    return;
                }
            }

            if (!Directory.Exists(Config.OutputPath))
            {
                Error(new Exception("FOLDER DOES NOT EXIST"));
                noerror = false;
                return;
            }

            IList<TitleStorageBlobMetadata> _saveData = [];

            Log("Loading Xbox live credentials");
            LoadXblTokenCredentials();

            if (Config.DeviceToken == null || Config.UserToken == null || (Config.UserToken == Config.DeviceToken))
            {
                Error(new Exception("TOKENS WERE NULL"));
                noerror = false;
                return;
            }

            Log("Authenticating...");
            try
            {
                await AuthenticateXbl();
            }
            catch (XboxAuthException E)
            {
                Error(E);
                noerror = false;
                return;
            }
            Log("Fetching save data...");
            try
            {
                TitleStorageBlobMetadataResult blobMetadataResult = await _storageService.GetBlobMetadata();
                if (blobMetadataResult.pagingInfo.continuationToken != null)
                {
                    throw new Exception("Final token is not null, wrong implementation");
                }
                _saveData = blobMetadataResult.Blobs;

            }
            catch (Exception e)
            {
                Error(e);
                noerror = false;
                return;
            }

            var blobMetadata = new Dictionary<string, TitleStorageAtomMetadataResult>();
            foreach (TitleStorageBlobMetadata blob in _saveData)
            {
                TitleStorageAtomMetadataResult atomMetadata = await _storageService.GetBlobAtoms(blob.FileName);
                blobMetadata.Add(blob.FileName, atomMetadata);
            }

            var blobCount = blobMetadata.Count;
            var atomCount = blobMetadata.Sum(a => a.Value.Atoms.Count);

            Log($"Found {blobCount} blobs with {atomCount} atoms");
            Downloader.main.ProgressBarUpdate(0, atomCount);
            Log($"Saving in the output folder: {Config.OutputPath}");

            Log("Preparing for download");
            //creating meta files for partial download support
            if (!partial)
            {
                using (FileStream fs = new(Path.Combine(Config.OutputPath, Config.MetaFileName), FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    await sw.WriteLineAsync("file to support partial downloads using spark save downloader");
                }
            }

            foreach (TitleStorageBlobMetadata blob in _saveData)
            {
                var atomMetadata = await _storageService.GetBlobAtoms(blob.FileName);
                foreach (string atom in atomMetadata.Atoms.Keys)
                {
                    var atomId = atomMetadata.Atoms[atom];
                    var filename = atom;
                    var filePath = Path.Combine(Config.OutputPath, filename);


                    if (partial)
                    {
                        if (File.Exists(filePath))
                        {
                            if (new FileInfo(filePath).Length != 0)
                            {
                                Log($"skipping {filename} already exists");
                                continue;
                            }
                            else
                            {
                                try
                                {
                                    File.Delete(filePath);
                                }
                                catch (Exception ex)
                                {
                                    Error(ex);
                                    noerror = false;
                                    Log($"skipping {filename} unable delete the invalid file");
                                    continue;
                                }
                            }
                        }
                    }

                    Log($"Downloading: {filename}");

                    byte[] atomData = null;
                    try
                    {
                        atomData = await DownloadAtomData(atomId);
                    }
                    catch (Exception e)
                    {
                        Log("exception encountered when downloading");
                        Log(filePath);
                        Log(blob.FileName);
                        Error(e);
                        noerror = false;

                        DialogResult res = MessageBox.Show($"downloading {filename} failed due to an xbox authentication issue.\nretry?\n\nyes: retry downloading the same file\nno: skip the file\ncancel: cancel all download", "Download error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
                        if (res == DialogResult.Yes)
                        {
                            bool retry = true;
                            bool first = true;
                            while (retry)
                            {
                                DialogResult again = DialogResult.Yes;
                                if (!first)
                                {
                                    again = MessageBox.Show("Download failed. try again?", "Download error", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                }
                                first = false;
                                if (again == DialogResult.Yes)
                                {
                                    Log("Loading Xbox live credentials");
                                    LoadXblTokenCredentials();
                                    if (Config.DeviceToken == null || Config.UserToken == null || (Config.UserToken == Config.DeviceToken))
                                    {
                                        Error(new Exception("TOKENS WERE NULL"));
                                        continue;
                                    }
                                    Log("Authenticating...");
                                    try
                                    {
                                        await AuthenticateXbl();
                                    }
                                    catch (XboxAuthException E)
                                    {
                                        Error(E);
                                        continue;
                                    }
                                    try
                                    {
                                        Log($"Retrying downloading: {filename}");
                                        atomData = await DownloadAtomData(atomId);
                                    }
                                    catch (Exception a)
                                    {
                                        Error(a);
                                        continue;
                                    }
                                }
                                else
                                {
                                    retry = false;
                                }
                            }
                        }
                        else if (res == DialogResult.No) continue;
                        else if (res == DialogResult.Cancel) return;

                    }

                    try
                    {
                        //saving
                        using (FileStream fs = new(filePath, FileMode.CreateNew))
                        {
                            await fs.WriteAsync(atomData);
                        }
                    }
                    catch (Exception e)
                    {
                        noerror = false;
                        Error(e);
                    }
                    Downloader.main.ProgressBarUpdate();
                }
            }

            //cleaning up
            try
            {
                Log("Cleaning up");
                if (noerror) File.Delete(Path.Combine(Config.OutputPath, Config.MetaFileName));
            }
            catch (Exception e)
            {
                Error(e);
            }
            Log("FINISHED");
            MessageBox.Show(Config.FinalMessage, "Thanks");
            return;
        }


    }
}
