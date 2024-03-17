using System;

using PracticeMedicine.Aridity;

using System.Windows;

using Squirrel;

using System.Linq;
using System.Threading.Tasks;

namespace Aridity.Services
{
    sealed class UpdateService : Service, IUpdateService
    {
        public UpdateService() 
        {
        }

        public void Run()
        {
            using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/PracticeMedicine03/aridity"))
            {
                Task.Run(async () =>
                {
                    Console.WriteLine("[ UPDATE SERVICE ] Checking for updates...");
                    var result = mgr.Result.CheckForUpdate();

                    if (result.Result.ReleasesToApply.Any())
                    {
                        Console.WriteLine($"[ UPDATE SERVICE ] Update available!\n" +
                            $"[ UPDATE SERVICE ] Current version is {result.Result.CurrentlyInstalledVersion}\n" +
                            $"[ UPDATE SERVICE ] New version is {result.Result.FutureReleaseEntry}");
                        MessageBoxResult msgBoxResult = MessageBox.Show("There are updates available for Aridity.\nDo you want to update now?", "Aridity", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
                        if (msgBoxResult == MessageBoxResult.Yes)
                        {
                            await mgr.Result.UpdateApp();
                        }
                    }
                });
            }
        }

        protected override void OnServiceCreated(Service service)
        {
            SvcName = "UpdateService";
            SvcClass = this;

            base.OnServiceCreated(this);
        }
    }
}
