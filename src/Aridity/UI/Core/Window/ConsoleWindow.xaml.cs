using System;
using System.Linq;
using System.Windows;
using PracticeMedicine.Aridity;

namespace Aridity
{
    /// <summary>
    /// Interaction logic for ConsoleWindow.xaml
    /// </summary>
    public partial class ConsoleWindow : Window
    {
        public ConsoleWindow()
        {
            InitializeComponent();

            this.consoleLog.Text = "Welcome to Aridity\n" +
                "Current version branch: beta prerelease\n" +
                "This multi-purpose developer console can help you catch errors or enter commands.\n" +
                "Use the 'help' command to show all of the Aridity console commands.";

            if (AridityF.Log.ToString().Any())
            {
                this.consoleLog.Text = this.consoleLog.Text + "";

            }

            this.consoleLog.IsReadOnly = true;

            // check if the IsConsoleAlreadyOpened bool is set to null
            if(!StartupFunctions.IsConsoleAlreadyOpened)
            {
                StartupFunctions.IsConsoleAlreadyOpened = true;
            }
        }

        private void btnEnterCommand_Click(object sender, RoutedEventArgs e)
        {
            this.consoleLog.Text = this.consoleLog.Text + "\n" + "> " + this.consoleCommand.Text;

            if(this.consoleCommand.Text == "help")
            {
                this.consoleLog.Text = this.consoleLog.Text + "\nhelp\n" +
                    "Aridity commands\n" +
                    "help - show this help prompt\n" +
                    "clear (or deleteLog, cls) - clear the console log\n" +
                    "exit (or quit) - close Aridity";
            }
            else if (this.consoleCommand.Text == "help args")
            {
                this.consoleLog.Text = this.consoleLog.Text + "\nhelp\n" +
                    "Aridity Command-line arguments\n" +
                    "--console - show the developer console\n" +
                    "--developer - enable developer features/options\n" +
                    "--disable-discord - disable Discord RPC (can be reenabled at the settings)";
            }
            else if (this.consoleCommand.Text == "clear" || this.consoleCommand.Text == "deleteLog"
                || this.consoleCommand.Text == "cls")
            {
                this.consoleLog.Text = "Welcome to Aridity\n" +
                "Current version branch: beta prerelease\n" +
                "This multi-purpose developer console can help you catch errors or enter commands.\n" +
                "Use the 'help' command to show all of the Aridity console commands.";
            }
            else if (this.consoleCommand.Text == "exit" || this.consoleCommand.Text == "quit")
            {
                Application.Current.Shutdown();
            }
            else
            {
                this.consoleLog.Text = this.consoleLog.Text + "\nCould not load command: " +
                    this.consoleCommand.Text + ". Make sure you've entered the correct command.\n" +
                    "Use the 'help' command to show all of the commands";
            }
        }
    }
}
