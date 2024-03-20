using System;
using PracticeMedicine.Aridity.Services;

namespace Aridity.Services
{
    public class UpdateService
    {
        static IUpdateService Service
        {
            get { return ServiceSingleton.GetRequiredService<IUpdateService>(); }
        }

        public static void CheckForUpdates()
        {
            Service.CheckForUpdates();
        }
    }
}
