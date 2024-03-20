using System;
using System.Linq;
using Aridity.Services;
using PracticeMedicine.Aridity;
using PracticeMedicine.Aridity.Util;

namespace Aridity
{
    public static class StartupFunctions
    {
        private static string[] args = Environment.GetCommandLineArgs();

        public static bool IsConsoleAlreadyOpened;

        public static void Start()
        {
            if(args.Contains("--console"))
            {
                ConsoleWindow console = new ConsoleWindow();
                console.Show();
            }

            AridityF.Log.Warn("Starting up...");

            var container = new AridityServiceContainer();
            container.AddService(typeof(IUpdateService), new UpdateService());
        }
    }
}
