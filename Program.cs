using System.Net;
using System.Runtime.InteropServices;
using System.IO.Compression;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using UC_Update_Downloader;
using Newtonsoft.Json;
using System.Security.Principal;

namespace UC_Update_Downloader
{
    public class Program
    {
        // Fr this is like the messiest code I've ever written lol..

        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void Main(string[] args)
        {

            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_CLOSE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }

            const int SW_HIDE = 0;
            const int SW_SHOW = 5;

            ShowWindow(handle, SW_SHOW);





            Console.Title = "Chill Update V0.1.0 | ChobbyCode";

            var text = @"
             ___ _    _ _   _      _   _           _       _
            / __| |  (_) | | |    | | | |         | |     | |  V0.1.0
            | | | |__ _| | | |    | | | |____   __| | __,_| |_  ___
            | | |    | | | | |    | | | | ._ \ / _. |/ _  | __|/ _ \
            | |_| || | | |_| |._  | |_| | |_) | (_| | (_| | |_|  __/  _  _
            \___|_||_|_|___|__(_)  \___/| .__/ \__._|\__,_|\__|\__(_)(_)(_)
                                        | |                     
                                        | |
                                        |_|

            ";

            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine(text);

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("             Credits: ChobbyCode (Application), DanielSWolf (Progress Bar)");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("               https://gist.github.com/DanielSWolf/0ab6a96899cc5377bf54");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;


            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string openDir = basePath;



            string configFileLoc = Path.GetFullPath(Path.Combine(basePath, @"..\")) + @"\_config.json";
            if (File.Exists(configFileLoc))
            {
                // If this is a run from config app then you can do the install
                downloaderFromConfig(configFileLoc);
            }
            else
            {

                UCCommandLine commandLine = new UCCommandLine();

                commandLine.newCommandLine();

            }
        }

        public static void downloaderFromConfig(string configFileLoc)
        {
            string configText = File.ReadAllText(configFileLoc);

            ConfigFile configJSON = JsonConvert.DeserializeObject<ConfigFile>(configText);


            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = basePath + "\\.zip\\download.zip";

            newDir(basePath + "\\.zip");
            deleteDir("\\temp");
            newDir(basePath + "\\temp");

            string path = configJSON.URL + @"/zipball/Download";
            Console.Write("Fetching Data From: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(path);
            Console.ForegroundColor = ConsoleColor.White;
            using (var client = new WebClient())
            {
                client.DownloadFile(path, fullPath);
            }

            ZipFile.ExtractToDirectory(fullPath, basePath + "\\temp\\");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Download Successful");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
            Console.WriteLine("Extracting Contents..");

            if (Directory.GetDirectories(basePath + "\\temp").Count() > 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed To Extract");
                Console.WriteLine("ERROR: Too many packages to handle.");

                return;
            }
            else
            {
                string fileLoc = "";
                foreach (string dir in Directory.GetDirectories(basePath + "temp"))
                {
                    Console.WriteLine(dir);
                    fileLoc = dir;
                }

                if (!File.Exists(fileLoc + "\\uc_config.json"))
                {
                    //Return if the file doesn't exist
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed To Extract");
                    Console.WriteLine("ERROR: Couldn't find config file");

                    return;
                }

                string JSONText = File.ReadAllText(fileLoc + "\\uc_config.json");

                JObject ConfigData = JObject.Parse(JSONText);

                IList<JToken> locationInfo = ConfigData["ExtractInfo"]["Data"].Children().ToList();

                IList<InstallInfo> moveInfo = new List<InstallInfo>();
                foreach (JToken result in locationInfo)
                {
                    // JToken.ToObject is a helper method that uses JsonSerializer internally
                    InstallInfo searchResult = result.ToObject<InstallInfo>();
                    moveInfo.Add(searchResult);
                }

                List<string> fromFiles = new List<string>();
                List<string> toFiles = new List<string>();
                List<string> fileNames = new List<string>();

                foreach (InstallInfo info in moveInfo)
                {
                    Console.WriteLine(info!.From);
                    Console.WriteLine(info!.To);

                    fileNames.Add(info!.FileName);

                    Console.Write("Parsing Files..");


                    if (info!.From[0].ToString() == "*")
                    {
                        //This is the easiest one lol
                        string fileEnd = "";
                        string character = "";
                        int val = 2;
                        while (val < info!.From.Length)
                        {
                            character = info!.From[val].ToString();

                            fileEnd = fileEnd + character;

                            val++;
                        }

                        Console.WriteLine(" Complete.");

                        fromFiles.Add(fileLoc + @"\" + fileEnd);
                    }
                    else
                    {
                        if (info!.From[0].ToString() + info!.From[1].ToString() == "..")
                        {
                            string newPath = Path.GetFullPath(Path.Combine(basePath, @"..\"));

                            string fileEnd = "";
                            string character = "";
                            int val = 2;
                            while (val < info!.From.Length)
                            {
                                character = info!.From[val].ToString();

                                fileEnd = fileEnd + character;

                                val++;
                            }

                            newPath = newPath + fileEnd;

                            Console.WriteLine(" Complete.");

                            fromFiles.Add(newPath);
                        }
                    }


                    if (info!.To[0].ToString() == "*")
                    {
                        //This is the easiest one lol
                        string fileEnd = "";
                        string character = "";
                        int val = 2;
                        while (val < info!.To.Length)
                        {
                            character = info!.To[val].ToString();

                            fileEnd = fileEnd + character;

                            val++;
                        }

                        Console.WriteLine(" Complete.");

                        toFiles.Add(fileLoc + @"\" + fileEnd);
                    }
                    else
                    {
                        if (info!.To[0].ToString() + info!.To[1].ToString() == "..")
                        {
                            string newPath = Path.GetFullPath(Path.Combine(basePath, @"..\"));

                            string fileEnd = "";
                            string character = "";
                            int val = 2;
                            while (val < info!.To.Length)
                            {
                                character = info!.To[val].ToString();

                                fileEnd = fileEnd + character;

                                val++;
                            }

                            newPath = newPath + fileEnd;

                            Console.WriteLine(" Complete.");

                            toFiles.Add(newPath);
                        }
                    }

                }

                int i = 0;
                foreach (string location in fromFiles)
                {
                    Console.WriteLine(location);

                    string endPath = toFiles[i];
                    Console.WriteLine(endPath);

                    if (!Directory.Exists(endPath))
                    {
                        newDir(endPath);
                    }

                    try
                    {
                        File.Copy(location, endPath + fileNames[i], true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    i++;
                }

            }
        }

        public static void newDir(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Console.WriteLine("Creating Directory: " + dir);
                Directory.CreateDirectory(dir);
            }
        }

        public static void deleteDir(string rootPath)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            if (Directory.Exists(basePath + rootPath))
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(basePath + rootPath);

                foreach (FileInfo file in di.GetFiles())
                {
                    Console.WriteLine("Deleting File: " + file);
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    Console.WriteLine("Deleting File: " + dir);
                    dir.Delete(true);
                }

                if (Directory.Exists(basePath + rootPath))
                {
                    Directory.Delete(basePath + rootPath);
                }
            }
        }
    }
}