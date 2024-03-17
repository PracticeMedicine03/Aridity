using System;
using Aridity.Services;
using PracticeMedicine.Aridity;
using PracticeMedicine.Aridity.Sda;

namespace Aridity
{
    internal class StartupFunctions
    {
        public static void Start()
        {
            var container = new AridityServiceContainer();
            container.AddService(typeof(IUpdateService), new UpdateService());
        }
    }
}
