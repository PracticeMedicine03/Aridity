using System;
using Aridity.Services;
using PracticeMedicine.Aridity;
using PracticeMedicine.Aridity.Util;

namespace Aridity
{
    public class StartupFunctions
    {
        public static void Start()
        {
            AridityF.Log.Warn("Starting up...");

            var container = new AridityServiceContainer();
            container.AddService(typeof(IUpdateService), new UpdateService());
        }
    }
}
