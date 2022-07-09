﻿//Roblox Tweaker C# Edition

namespace RTCSharpEdition {
    class Program {
        const string NAME = "Roblox Tweaker C# Edition";
        const string VERSION = "1.1";
        const string AUTHOR = "OhRetro";
        const string NAME_VERSION = NAME+" v"+VERSION;
        const string REPOSITORY = "https://github.com/OhRetro/Roblox-Tweaker-CSharp-Edition";
        static string RO_VERSIONS_DIR = System.Environment.GetEnvironmentVariable("LOCALAPPDATA")+"\\Roblox\\Versions";
        static string RO_VERSION_DIR = "Not Set";
        static string RO_VERSION_DIR_TYPE = "Not Set";
        const string RO_VERSION_DIR_FILE = "./roverdir.txt";
        static string RO_TEXTURE_DIR = "Not Set";
        static string[] EXCEPTION_TEXTURES = {"sky", "brdfLUT.dds", "studs.dds", "wangIndex.dds"};
        const string BACKUP_DIR = "./TextureBackup";
        const string PATH_TO_TEXTURES_DIR = "\\PlatformContent\\pc\\textures";

        //Copy Folder
        static void CopyFolder(string sourceFolder, string destFolder) {
            if (!Directory.Exists(destFolder)) {
                Directory.CreateDirectory(destFolder);
            }
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files) {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest);
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders) {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }
        
        //Select Roblox Version Directory
        static void SelectRoVersion() {
            Console.Clear();
            string[] dirs = Directory.GetDirectories(RO_VERSIONS_DIR);
            string[] valid_dirs = {};
            for (int i = 0; i < dirs.Length; i++) {
                if (dirs[i].Split('\\').Last().StartsWith("version-")) {
                    valid_dirs = valid_dirs.Append(dirs[i]).ToArray();
                }
            }
            int choice;
            do {
                Console.WriteLine("Select a Roblox version to use:");
                for (int i = 0; i < valid_dirs.Length; i++) {
                    Console.Write("[{0}] {1} | ", i, valid_dirs[i].Split('\\').Last());
                    Console.Write("{0} | ", RoVersionType(valid_dirs[i]));
                    Console.Write("Textures Count: {0} | ", TextureCount(valid_dirs[i]+PATH_TO_TEXTURES_DIR));
                    Console.Write("{0}\n", Directory.GetCreationTime(valid_dirs[i]));
                }
                
                Console.Write(">");
                try {
                    choice = Convert.ToInt32(Console.ReadLine());
                } catch (Exception) {
                    choice = -1;
                }

                Console.Clear();

            } while (choice < 0 || choice >= valid_dirs.Length);
            RO_VERSION_DIR = valid_dirs[choice];
        }

        //Write File
        static void WriteRoVersionDirFile() {
            Console.WriteLine("[Writing File]");
            File.WriteAllText(RO_VERSION_DIR_FILE, RO_VERSION_DIR);
            Thread.Sleep(300);
        }

        //Read File
        static void ReadRoVersionDirFile() {
            if (File.Exists(RO_VERSION_DIR_FILE)) {
                Console.WriteLine("[Reading File]");
                RO_VERSION_DIR = File.ReadAllText(RO_VERSION_DIR_FILE);
                RO_TEXTURE_DIR = RO_VERSION_DIR+PATH_TO_TEXTURES_DIR;
                RO_VERSION_DIR_TYPE = RoVersionType(RO_VERSION_DIR);
                Thread.Sleep(300);
            } else {
                Console.WriteLine("[File not found]");
                Thread.Sleep(1000);
                SelectRoVersion();
                WriteRoVersionDirFile();
                ReadRoVersionDirFile();
            }
        }

        //Manual Update File
        static void ManualDirUpdate() {
            SelectRoVersion();
            WriteRoVersionDirFile();
            ReadRoVersionDirFile();
            Console.Clear();
            Console.WriteLine("[Directory Updated]\n");
        }

        //Remove Texture
        static void RemoveTexture() {
            int choice;
            do {
                Console.WriteLine("Do you want to remove all textures or leave some necessary textures?");
                Console.WriteLine("[1] Leave Necessary Textures \n[2] Remove All\n[0] Cancel");

                Console.Write(">");
                try {
                    choice = Convert.ToInt32(Console.ReadLine());
                } catch (Exception) {
                    choice = -1;
                }
                Console.Clear();
            } while (choice < 0 || choice > 2);

            Console.Clear();

            if (choice == 0) {
                Console.WriteLine("[Operation Cancelled]\n");
                return;   

            } else if (choice != 0) {
                string[] dirs = Directory.GetDirectories(RO_TEXTURE_DIR);
                string[] files = Directory.GetFiles(RO_TEXTURE_DIR);

                string[] list_textures = {};
                for (int i = 0; i < dirs.Length; i++) {
                    list_textures = list_textures.Append(dirs[i]).ToArray();
                }
                for (int i = 0; i < files.Length; i++) {
                    list_textures = list_textures.Append(files[i]).ToArray();
                }
 
                if (choice == 1) {
                    for (int i = 0; i < EXCEPTION_TEXTURES.Length; i++) {
                        if (list_textures.Contains(RO_TEXTURE_DIR+"\\"+EXCEPTION_TEXTURES[i])) {
                            list_textures = list_textures.Where(x => x != RO_TEXTURE_DIR+"\\"+EXCEPTION_TEXTURES[i]).ToArray();
                        }
                    }
                }

                Console.WriteLine("[Removing Textures]\n");
                for (int i = 0; i < list_textures.Length; i++) {
                    if (Directory.Exists(list_textures[i])) {
                        Directory.Delete(list_textures[i], true);
                    } else if (File.Exists(list_textures[i])) {
                        File.Delete(list_textures[i]);
                    }
                    Console.WriteLine("Removed {0}", list_textures[i].Split('\\').Last());
                }
                Console.Clear();

                string[] dirs_remaining = Directory.GetDirectories(RO_TEXTURE_DIR);
                string[] files_remaining = Directory.GetFiles(RO_TEXTURE_DIR);

                string[] list_textures_remaining = {};
                for (int i = 0; i < dirs_remaining.Length; i++) {
                    list_textures_remaining = list_textures_remaining.Append(dirs_remaining[i]).ToArray();
                }
                for (int i = 0; i < files_remaining.Length; i++) {
                    list_textures_remaining = list_textures_remaining.Append(files_remaining[i]).ToArray();
                }

                if (list_textures_remaining.Length > 0) {
                    Console.WriteLine("[Textures Removed]\n");
                } else if (list_textures_remaining.Length <= EXCEPTION_TEXTURES.Length) {
                    Console.WriteLine("[No Textures Remaining]\n");
                    return;
                }
            }
        }

        //List Texture
        static void ListTextures() {
            Console.WriteLine("[Textures List]");
            Console.WriteLine("Texture Count: {0}\n", TextureCount());
            string[] dirs = Directory.GetDirectories(RO_TEXTURE_DIR);
            string[] files = Directory.GetFiles(RO_TEXTURE_DIR);
            for (int i = 0; i < dirs.Length; i++) {
                Console.WriteLine("{0}", dirs[i].Split('\\').Last());
            }
            for (int i = 0; i < files.Length; i++) {
                Console.WriteLine("{0}", files[i].Split('\\').Last());
            }

            Console.Write("\n[Press Any key to continue]");
            Console.ReadKey();
            Console.Clear();
        }

        //Backup Texture
        static void BackupTextures() {
            Console.WriteLine("[Backing up Textures]");

            if (!Directory.Exists(BACKUP_DIR)) {
                Directory.CreateDirectory(BACKUP_DIR);
                Console.WriteLine("[Backup Folder Created]");
            } else {
                Console.WriteLine("[Backup Folder Already Exists]");
            }

            try {
                Console.WriteLine("[Copying Textures]");
                CopyFolder(RO_TEXTURE_DIR, BACKUP_DIR);
            } catch (Exception) {
                Console.WriteLine("[Backup Failed]");
            }

            Console.Write("\n[Press Any key to continue]");
            Console.ReadKey();
            Console.Clear();
        }

        //Restore Texture
        static void RestoreTextures() {
            Console.WriteLine("[Restoring Textures]");

            try {
                Console.WriteLine("[Copying Textures]");
                CopyFolder(BACKUP_DIR, RO_TEXTURE_DIR);
            } catch (Exception) {
                Console.WriteLine("[Restore Failed]");
            }

            Console.WriteLine("\n[Press Any key to continue]");
            Console.ReadKey();
            Console.Clear();
        }

        //Delete Backup
        static void DeleteBackupDir() {
            Console.WriteLine("[Deleting Backup Folder]");
            if (Directory.Exists(BACKUP_DIR)) {
                Directory.Delete(BACKUP_DIR, true);
                Console.WriteLine("[Backup Folder Deleted]");
            } else {
                Console.WriteLine("[Backup Folder Not Found]");
            }
            Console.WriteLine("\n[Press Any key to continue]");
            Console.ReadKey();
            Console.Clear();
        }

        //Get Roblox Type
        static string RoVersionType(string directory) {
            string[] files = Directory.GetFiles(directory);
            for (int i = 0; i < files.Length; i++) {
                if (files[i].Split('\\').Last() == "RobloxPlayerBeta.exe") {
                    return "Roblox Player";
                } else if (files[i].Split('\\').Last() == "RobloxStudioBeta.exe") {
                    return "Roblox Studio";
                }
            }
            return "Unknown";
        }

        //Count Backup Files
        static void BackupTexuresCount() {
            if (!Directory.Exists(BACKUP_DIR)) {
                Console.WriteLine("[Backup Folder Not Found]");
            } else {
                int count = TextureCount(BACKUP_DIR);
                Console.WriteLine("[Backup Textures Count]");
                Console.WriteLine("Count: {0}\n", count);

                if (count == 24) {
                    Console.WriteLine("[All Textures Found]");
                } else {
                    Console.WriteLine("[Not All Textures Found]\n[There are {0} Texture(s) Missing]", 24-count);
                }
            }

            Console.Write("\n[Press Any key to continue]");
            Console.ReadKey();
            Console.Clear();
        }

        //Count Textures
        static int TextureCount(string directory="") {
            if (directory == "") {
                directory = RO_TEXTURE_DIR;
            }

            string[] dirs = Directory.GetDirectories(directory);
            string[] files = Directory.GetFiles(directory);
            int count = 0;
            for (int i = 0; i < dirs.Length; i++) {
                count++;
            }
            for (int i = 0; i < files.Length; i++) {
                count++;
            }
            return count;
        }

        //About
        static void About() {
            Console.WriteLine("[About]");
            Console.WriteLine("{0}", NAME_VERSION);
            Console.WriteLine("Made by {0}", AUTHOR);
            Console.WriteLine("Repository: {0}", REPOSITORY);

            Console.Write("\n[Press Any key to continue]");
            Console.ReadKey();
            Console.Clear();
        }

        static void Main(string[] args) {
            Console.Title = NAME_VERSION;
            Console.Clear();
            
            Console.WriteLine("{0} by {1}\n", NAME_VERSION, AUTHOR);
            ReadRoVersionDirFile();

            if (!RO_VERSION_DIR.Split('\\').Last().StartsWith("version-")) {
                Console.WriteLine("[Invalid Roblox version directory]");
                Thread.Sleep(2000);
                ManualDirUpdate();
            }

            Console.Clear();
            
            int menu;
            do {
                Console.WriteLine(NAME_VERSION);
                Console.WriteLine("[1] Delete Textures\n[2] List Textures\n[3] Update Version Directory");
                Console.WriteLine("[4] Backup Textures\n[5] Restore Textures\n[6] Delete Backup Folder");
                Console.WriteLine("[7] Backup Textures Count\n");
                Console.WriteLine("[8] About\n[0] Exit\n");
                Console.WriteLine("Current Version Directory:\n\"{0}\"\nType: {1}", RO_VERSION_DIR, RO_VERSION_DIR_TYPE);
                
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
                        ListTextures();
                        break;
                    case 3:
                        ManualDirUpdate();
                        break;
                    case 4:
                        BackupTextures();
                        break;
                    case 5:
                        RestoreTextures();
                        break;
                    case 6:
                        DeleteBackupDir();
                        break;
                    case 7:
                        BackupTexuresCount();
                        break;
                    case 8:
                        About();
                        break;
                    default:
                        break;
                }
            } while (menu != 0);
        }
    }
}