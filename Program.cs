using System.Net;
//Roblox Tweaker C# Edition

using System;
using System.IO;

namespace RTCSharpEdition {
    class Program {
        const string NAME = "Roblox Tweaker C# Edition";
        const string VERSION = "0.1";
        const string NAME_VERSION = NAME+" v"+VERSION;
        static string RO_VERSIONS_DIR = System.Environment.GetEnvironmentVariable("LOCALAPPDATA")+"\\Roblox\\Versions";
        static string RO_VERSION_DIR = "Not set";
        static string RO_VERSION_DIR_FILE = "./roverdir.txt";

        static void RemoveTexture() {
            Console.WriteLine("[Working at it]");
        }

        static void Main(string[] args) {
            Console.Title = NAME_VERSION;
            Console.Clear();

            if (!File.Exists(RO_VERSION_DIR_FILE)) {
                Console.Clear();
                File.WriteAllText(RO_VERSION_DIR_FILE, RO_VERSIONS_DIR);
            }
            
            RO_VERSION_DIR = File.ReadAllText(RO_VERSION_DIR_FILE);
            
            int menu;
            do {
                Console.WriteLine(NAME_VERSION);
                Console.WriteLine("[1]Delete Texture\n[0]Exit\n");
                Console.WriteLine("Current Version Directory: {0}\n", RO_VERSION_DIR);

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
                    case 2:
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