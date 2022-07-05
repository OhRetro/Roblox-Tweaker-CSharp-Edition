//Roblox Tweaker C# Edition

using System;
using System.IO;
using System.Threading;

namespace RTCSharpEdition {
    class Program {
        const string NAME = "Roblox Tweaker C# Edition";
        const string VERSION = "0.1";
        const string AUTHOR = "OhRetro";
        const string NAME_VERSION = NAME+" v"+VERSION;
        static string RO_VERSIONS_DIR = System.Environment.GetEnvironmentVariable("LOCALAPPDATA")+"\\Roblox\\Versions";
        static string RO_VERSION_DIR = "Not Set";
        static string RO_VERSION_DIR_TYPE = "Feature Not Implemented";
        const string RO_VERSION_DIR_FILE = "./roverdir.txt";

        static void SelectRoVersion() {
            Console.Clear();
            string[] dirs = Directory.GetDirectories(RO_VERSIONS_DIR);
            int choice;
            do {
                Console.WriteLine("Select a Roblox version to use:");
                for (int i = 0; i < dirs.Length; i++) {
                    Console.WriteLine("["+i+"] "+dirs[i]);
                }
                
                Console.Write(">");
                try {
                    choice = Convert.ToInt32(Console.ReadLine());
                } catch (Exception) {
                    choice = -1;
                }

                Console.Clear();

            } while (choice < 0 || choice >= dirs.Length);
            RO_VERSION_DIR = dirs[choice];
        }

        static void WriteRoVersionDirFile() {
            Console.WriteLine("Writing...");
            File.WriteAllText(RO_VERSION_DIR_FILE, RO_VERSION_DIR);
            Thread.Sleep(300);
        }

        static void ReadRoVersionDirFile() {
            if (File.Exists(RO_VERSION_DIR_FILE)) {
                Console.WriteLine("Reading...");
                RO_VERSION_DIR = File.ReadAllText(RO_VERSION_DIR_FILE);
                Thread.Sleep(300);
            } else {
                Console.WriteLine("File not found.");
                Thread.Sleep(1000);
                SelectRoVersion();
                WriteRoVersionDirFile();
            }
        }

        static void RemoveTexture() {
            Console.WriteLine("[WIP]");
        }

        static void Main(string[] args) {
            Console.Title = NAME_VERSION;
            Console.Clear();
            
            Console.WriteLine(NAME+" made by "+AUTHOR);
            ReadRoVersionDirFile();
            Console.Clear();
            
            int menu;
            do {
                Console.WriteLine(NAME_VERSION);
                Console.WriteLine("[1]Delete Texture\n[3]Update Version Directory\n[0]Exit\n");
                Console.WriteLine("Current Version Directory:\n\"{0}\"\nType: {1}\n", RO_VERSION_DIR, RO_VERSION_DIR_TYPE);
                
                Console.Write(">");
                try {
                    menu = Convert.ToInt32(Console.ReadLine());
                } catch (Exception) {
                    menu = -1;
                }
                
                Console.Clear();
                
                switch (menu) {
                    case 1:
                        RemoveTexture();
                        break;
                    case 3:
                        SelectRoVersion();
                        WriteRoVersionDirFile();
                        ReadRoVersionDirFile();
                        Console.Clear();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("[Invalid Input]");
                        break;
                }
            } while (menu != 0);
        }
    }
}