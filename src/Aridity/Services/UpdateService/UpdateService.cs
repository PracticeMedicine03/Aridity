using System;

using PracticeMedicine.Aridity;

using System.Windows;

using Squirrel;

using System.Linq;
using System.Threading.Tasks;
using PracticeMedicine.Aridity.Services;

namespace Aridity.Services
{
    sealed class UpdateService
    {
        static IUpdateService Service
        {
            get { return ServiceSingleton.GetRequiredService<IUpdateService>(); }
        }

        public void Run()
        {
            Service.Run();
        }
    }
}
