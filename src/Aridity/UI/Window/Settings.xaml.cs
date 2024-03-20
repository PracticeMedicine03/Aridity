using System;
using PracticeMedicine.Aridity;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Aridity
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        int currentGameID;

        public Settings()
        {
            InitializeComponent();
        }

        public Settings(int gameID)
        {
            InitializeComponent();

            AridityF.Log.InfoFormatted(
                "[ ARIDITY GUI ] Shown the Settings window with an gameID parameter of {0}", 
                gameID);

            currentGameID = gameID;

            this.Title = gameID.ToString() + " - Settings";
            
            if(Installer.IsGameDirectoryExists(gameID))
            {
                using (StreamReader sw = new StreamReader($"./cfg/{gameID}/config"))
                {
                    this.argTextBox.Text = sw.ReadLine();
                    sw.Close();
                }
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            AddSettingsTabsAsListBoxItems();
        }

        private void AddSettingsTabsAsListBoxItems()
        {
            this.settingsTabList.Items.Add("General");
            this.settingsTabList.Items.Add("Game files");
        }

        private void settingsTabList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.settingsTabList.SelectedIndex == 0)
            {
                this.settingsTabControl.SelectedIndex = 0;
            }
            else if (this.settingsTabControl.SelectedIndex == 1)
            {
                this.settingsTabControl.SelectedIndex = 1;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if(currentGameID == 4360)
            {
                if (!Directory.Exists($"./cfg/{currentGameID}"))
                {
                    Directory.CreateDirectory($"./cfg/{currentGameID}");

                    // check again
                    if(Directory.Exists($"./cfg/{currentGameID}"))
                    {
                        using (StreamWriter sw = new StreamWriter($"./cfg/{currentGameID}/config"))
                        {
                            sw.WriteLine(this.argTextBox.Text);
                        }
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter($"./cfg/{currentGameID}/config"))
                    {
                        sw.WriteLine(this.argTextBox.Text);
                    }
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (currentGameID == 4360)
            {
                if (!Directory.Exists($"./cfg/{currentGameID}"))
                {
                    Directory.CreateDirectory($"./cfg/{currentGameID}");
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter($"./cfg/{currentGameID}/config"))
                    {
                        sw.WriteLine(this.argTextBox.Text);
                    }
                }
            }
        }
    }
}
