using System;
using Squirrel;
using System.Linq;
using System.Windows;
using System.Threading.Tasks;

namespace Aridity.Services
{
    public class SquirrelUpdaterUpdateService : IUpdateService
    {
        public SquirrelUpdaterUpdateService() { }

        public void CheckForUpdates()
        {
            using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/PracticeMedicine03/aridity"))
            {
                Task.Run(async () =>
                {
                    PracticeMedicine.Aridity.AridityF.Log.Info("[ UPDATE SERVICE ] Checking for updates...");
                    var result = mgr.Result.CheckForUpdate();

                    if (result.Result.ReleasesToApply.Any())
                    {
                        PracticeMedicine.Aridity.AridityF.Log.Info($"[ UPDATE SERVICE ] Update available!\n" +
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
    }
}
