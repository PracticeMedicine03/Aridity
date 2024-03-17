using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aridity
{
    public class Git
    {
        public static bool error;

        // The Clone function is used for sourcemods only
        // Most sourcemods in GitHub (and GitLab) use the source code root/repository root directory
        // instead of it being a release
        private static void StartGit(string extraArgs)
        {
            Process git = new Process();
            //git.FileName = String.Format("{0}/bin/git/cmd/git.exe", System.Windows.Application.ResourceAssembly.Location);
            git.StartInfo.FileName = "./bin/git/cmd/git.exe";
            git.StartInfo.UseShellExecute = false;
            git.StartInfo.Arguments = extraArgs;
            git.StartInfo.CreateNoWindow = true;
            git.StartInfo.RedirectStandardOutput = true;
            git.StartInfo.RedirectStandardError = true;

            try
            {
                //if(Installer.IsRunning(git))
                //{
                //    git.Start();
                //}
                //else
                //{
                //    Console.WriteLine("Another instance is running!");
                //    MessageBox.Show("Another Git instance is already running!", "Aridity", MessageBoxButton.OK, MessageBoxImage.Error);
                //}
                git.Start();
            }
            catch (Exception ex)
            {
                error = true;
                Console.WriteLine(ex);
            }
        }

        public static void Clone(string link)
        {
            StartGit($"clone {link}");
        }

        public static void Clone(string link, string directory)
        {
            StartGit($@"clone {link} ""{directory}""");
        }

        public static void Clone(string link, string directory, bool isShallow)
        {
            if(isShallow)
            {
                StartGit($@"clone --depth 1 {link} ""{directory}""");
            }
            else
            {
                Console.WriteLine("[ GIT ] Warning! isShallow is not set to true. This may cause issues cloning");
                StartGit($@"clone {link} ""{directory}""");
            }
        }
    }
}
