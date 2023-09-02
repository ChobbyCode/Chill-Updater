using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO.Compression;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using UC_Update_Downloader;

namespace UC_Update_Downloader
{
    public class UCCommandLine
    {

        public bool runApp = true;
        public string basePath = AppDomain.CurrentDomain.BaseDirectory;
        public string openDir = "";

        public void newCommandLine()
        {

            Console.WriteLine("Chill Update Console © 2023 ChobbyCode. MIT License. UC-Addon.\nType help to view a list of basic commands.");
            Console.WriteLine();

            openDir = basePath;

            while (runApp)
            {
                Console.Write(openDir + ":> ");

                command(Console.ReadLine());
            }
        }

        public void command(string theCommand)
        {
            string x = theCommand.ToLower();

            if (x == "") {
                
            }else if(x == "e" || x == "exit" || x == "quit")
            {
                runApp = false;
            }else if(x == "cls")
            {
                Console.Clear();
            }else if(x == "help")
            {
                Console.WriteLine("");
                Console.WriteLine("download [url/uri] | Downloads a VALID Chill Update Package from github.");
                Console.WriteLine("cls                | Clears the console");
                Console.WriteLine("e/exit/quit        | Closes the application");
                Console.WriteLine("");
            }
            else
            {
                try
                {
                        string basePath = AppDomain.CurrentDomain.BaseDirectory;
                        string fullPath = basePath + "\\.zip\\download.zip";

                        newDir(basePath + "\\.zip");
                        newDir(basePath + "\\downloads");

                        int inc = 0;
                        foreach (string dir in Directory.GetDirectories(basePath + @"downloads\"))
                        {
                            inc++;
                        }

                        newDir(basePath + "\\downloads\\" + "download" + inc);

                        string path = x.Remove(0, 8) + "\\zipball\\Download";
                        Console.Write("Fetching Data From: ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(path);
                        Console.ForegroundColor = ConsoleColor.White;
                        using (var client = new WebClient())
                        {
                            client.DownloadFile(path, fullPath);
                        }

                        ZipFile.ExtractToDirectory(fullPath, basePath + "\\downloads\\" + "download" + inc + "\\");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Download Successful");
                        Console.ForegroundColor = ConsoleColor.White;

                }
                catch(Exception e) {
                    Console.WriteLine(e);

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
