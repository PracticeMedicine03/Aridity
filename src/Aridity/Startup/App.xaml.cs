using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace Aridity
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if(e.Args.Contains("--console"))
            {
                AllocConsole();
            }
        }
    }
}
