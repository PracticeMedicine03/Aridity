using System;
using System.Linq;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Aridity
{
    public class Installer
    {
        public static int gameID { get; set; }
        public static bool IsUsingGit;

        // https://stackoverflow.com/a/262291
        public static bool IsRunning(Process process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            try
            {
                Process.GetProcessById(process.Id);
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }

        public static string SourceModsPath()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Valve\\Steam");

            if (key != null)
            {
                string path = key.GetValue("SourceModInstallPath").ToString();

                return path + "\\";
            }
            else
            {
                return string.Empty;
            }
        }

        public static bool IsGameDirectoryExists(int appid)
        {
            if(appid == 4360)
            {
                if(Directory.Exists(SourceModsPath() + "alternative_fortresses")) 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            else if (appid == 4310)
            {
                if (Directory.Exists(SourceModsPath() + "bf"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            else if (appid == 2350)
            {
                if (Directory.Exists(SourceModsPath() + "pf2"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void Install()
        {
            if(gameID == 4360)
            {
                if(!IsGameDirectoryExists(4360))
                {
                    IsUsingGit = true;
                    Git.Clone("https://github.com/Daisreich/alternative_fortresses.git", SourceModsPath() + "/alternative_fortresses", true);
                }
                else
                {
                    Console.WriteLine("already installed");
                }
            }

            else if (gameID == 4310)
            {
                if(!IsGameDirectoryExists(4310))
                {
                    IsUsingGit = true;
                    Git.Clone("https://github.com/Beta-Fortress-2-Team/bf.git", SourceModsPath() + "/bf", true);
                }
                else
                {
                    Console.WriteLine("already installed");
                }
            }

            else if (gameID == 2350)
            {
                if(!IsGameDirectoryExists(4310))
                {
                    IsUsingGit = true;
                    //Git.Clone("https://github.com/Daisreich/alternative_fortresses.git", SourceModsPath() + "/alternative_fortresses", true);
                }
                else
                {
                    Console.WriteLine("already installed");
                }
            }
        }
    }

    public class Steam
    {
        private static String installationFolder;

        public Steam()
        {
            installationFolder = fetchInstallationFolder();
        }

        public static bool isSteamInstalled()
        {
            if(Directory.Exists(fetchInstallationFolder()))
                return true;
            else 
                return false;
        }

        public static String getInstallationFolder()
        {
            return installationFolder;
        }

        private static String fetchInstallationFolder()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Valve\Steam\");

            //Seems like steam is either not installed or it's a 32bit system.
            // if (key == null)
            // {
            //     key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Valve\Steam\");
            // }

            //If the key is still null then steam just isn't installed.
            if (key == null)
                return null;
            else
                if (key.GetValueNames().Contains("SteamPath"))
                return key.GetValue("SteamPath").ToString();
            else
                return null;
        }

        public static bool startAppId(int appId, String additionalParams)
        {
            String steamExecutable = fetchInstallationFolder() + @"\steam.exe";
            Process launchProcess = new Process();
            launchProcess.StartInfo.FileName = steamExecutable;
            launchProcess.StartInfo.Arguments = "-applaunch " + appId + " " + additionalParams;
            return launchProcess.Start();

            //if(!Installer.IsRunning(launchProcess))
            //    return launchProcess.Start();
            //else
            //    return false;
        }
    }
}
