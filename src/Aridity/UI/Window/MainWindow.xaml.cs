using System;
using System.IO;
using System.Linq;

using DiscordRPC;
using DiscordRPC.Logging;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aridity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon m_notifyIcon;

        int currentGameID;
        long clientID = 1218754501151035443;

        private DiscordRpcClient client;

        string[] args = Environment.GetCommandLineArgs();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            InitializeNotifyIcon("Aridity is been minimized. Click the tray icon to show the window again", "Aridity");
            
            if(!args.Contains("--disable-discord"))
            {
                InitializeRPC();
            }

            //new Microsoft.Toolkit.Uwp.Notifications.ToastContentBuilder()
            //                    .AddText("Welcome to Aridity")
            //                    .AddText("Aridity is an game manager mainly for SourceMods just like Adastral with the same style as Steam.")
            //                    .Show();

            CreateListBoxItemsForAvailableGamesOrProjects();

            this.lblInstallStatus.Visibility = Visibility.Hidden;
            this.installProgress.Visibility = Visibility.Hidden;

            this.installProgress.Foreground = new SolidColorBrush(Color.FromRgb(6, 216, 243));

            if(args.Contains("--console") || args.Contains("--developer"))
            {
                this.btnShowDevConsole.Visibility = Visibility.Visible;
            }
            else
            {
                this.btnShowDevConsole.Visibility = Visibility.Hidden;
            }

            this.btnInstallUpdateUninstallPlay.IsEnabled = false;
            this.btnSettings.IsEnabled = false;

            if(!Steam.isSteamInstalled())
            {
                MessageBox.Show("Required component: Steam is not installed.\nPlease make sure that it is installed.\nIf it is installed correctly, try again",
                "Aridity",
                MessageBoxButton.OK, MessageBoxImage.Error);

                Application.Current.Shutdown();
            }
        }

        private void InitializeRPC()
        {
            client = new DiscordRpcClient(clientID.ToString());
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

            //Subscribe to events
            client.OnReady += (sender, e) =>
            {
                Console.WriteLine("Received Ready from user {0}", e.User.Username);
            };

            client.OnPresenceUpdate += (sender, e) =>
            {
                Console.WriteLine("Received Update! {0}", e.Presence);
            };

            //Connect to the RPC
            client.Initialize();

            //Set the rich presence
            //Call this as many times as you want and anywhere in your code.
            client.SetPresence(new RichPresence()
            {
                Details = "Aridity",
                State = "Not playing - In main-menu",
                Assets = new Assets()
                {
                    LargeImageKey = "image_large",
                    LargeImageText = "Lachee's Discord IPC Library",
                    SmallImageKey = "image_small"
                }
            });
        }

        private WindowState m_storedWindowState = WindowState.Normal;
        private void InitializeNotifyIcon(string text, string title)
        {
            // initialise code here
            m_notifyIcon = new System.Windows.Forms.NotifyIcon();
            m_notifyIcon.BalloonTipText = text;
            m_notifyIcon.BalloonTipTitle = title;
            m_notifyIcon.Text = title;
            m_notifyIcon.Icon = new System.Drawing.Icon("./assets/record.ico");
            m_notifyIcon.Click += new EventHandler(m_notifyIcon_Click);
        }

        void m_notifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = m_storedWindowState;
        }
        void CheckTrayIcon()
        {
            ShowTrayIcon(!IsVisible);
        }

        void ShowTrayIcon(bool show)
        {
            if (m_notifyIcon != null)
                m_notifyIcon.Visible = show;
        }

        private void UpdateRPC(string details, string state)
        {
            if(client.IsInitialized)
            {
                client.SetPresence(new RichPresence()
                {
                    Details = details,
                    State = state,
                });
            }
        }

        private void UpdateRPC(string details, string state, string largeImage)
        {
            if(client.IsInitialized)
            {
                client.SetPresence(new RichPresence()
                {
                    Details = details,
                    State = state,

                    Assets = new Assets()
                    {
                        LargeImageKey = largeImage,
                    }
                });
            }
        }

        private void UpdateRPC(string details, string state, string largeImage, string smallImage)
        {
            if (client.IsInitialized)
            {
                client.SetPresence(new RichPresence()
                {
                    Details = details,
                    State = state,

                    Assets = new Assets()
                    {
                        LargeImageKey = largeImage,
                        SmallImageKey = smallImage,
                    }
                });
            }
        }

        private void UpdateRPC(string details, string state, string largeImage, string smallImage, string largeImageText)
        {
            if (client.IsInitialized)
            {
                client.SetPresence(new RichPresence()
                {
                    Details = details,
                    State = state,

                    Assets = new Assets()
                    {
                        LargeImageKey = largeImage,
                        LargeImageText = largeImageText,

                        SmallImageKey = smallImage,
                    }
                });
            }
        }

        private void UpdateRPC(string details, string state, string largeImage, string smallImage, string largeImageText, string smallImageText)
        {
            if (client.IsInitialized)
            {
                client.SetPresence(new RichPresence()
                {
                    Details = details,
                    State = state,

                    Assets = new Assets()
                    {
                        LargeImageKey = largeImage,
                        LargeImageText = largeImageText,

                        SmallImageKey = smallImage,
                        SmallImageText = smallImageText,
                    }
                });
            }
        }

        private void CreateListBoxItemsForAvailableGamesOrProjects()
        {
            this.gameList.Items.Add("Alternative Fortresses");
            this.gameList.Items.Add("Beta Fortress");
            this.gameList.Items.Add("Pre-Fortress 2");
        }

        private void gameList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(this.gameList.SelectedItem.ToString() + " - " + this.gameList.SelectedIndex);

            if (this.gameList.SelectedIndex == 0)
            {
                currentGameID = 4360;

                if(!args.Contains("--disable-discord"))
                {
                    UpdateRPC("Alternative Fortresses", "Not playing", "aridityLarge", "ariditySmall-af", null, "Alternative Fortresses");
                }

                if (Installer.IsGameDirectoryExists(currentGameID))
                {
                    this.btnInstallUpdateUninstallPlay.Content = "Play";
                }
                else
                {
                    this.btnInstallUpdateUninstallPlay.Content = "Install";
                }

                this.lblGameName.Content = "Alternative Fortresses";
            }
            else if (this.gameList.SelectedIndex == 1)
            {
                currentGameID = 4310;

                if(!args.Contains("--disable-discord"))
                {
                    UpdateRPC("Beta Fortress", "Not playing", "aridityLarge", "ariditySmall-bf", null, "Beta Fortress");
                }

                if (Installer.IsGameDirectoryExists(currentGameID))
                {
                    this.btnInstallUpdateUninstallPlay.Content = "Play";
                }
                else
                {
                    this.btnInstallUpdateUninstallPlay.Content = "Install";
                }

                this.lblGameName.Content = "Beta Fortress";
            }
            else if (this.gameList.SelectedIndex == 2)
            {
                currentGameID = 2350;

                if(!args.Contains("--disable-discord"))
                {
                    UpdateRPC("Pre-Fortress 2", "Not playing", "aridityLarge", "ariditySmall-af", null, "Pre-Fortress 2");
                }

                if (Installer.IsGameDirectoryExists(currentGameID))
                {
                    this.btnInstallUpdateUninstallPlay.Content = "Play";
                }
                else
                {
                    this.btnInstallUpdateUninstallPlay.Content = "Install";
                }
                this.lblGameName.Content = "Pre-Fortress 2";
            }

            if(currentGameID.ToString().Any())
            {
                this.btnInstallUpdateUninstallPlay.IsEnabled = true;

                this.btnSettings.IsEnabled = true;
            }
        }

        private void btnInstallUpdateUninstallPlay_Click(object sender, RoutedEventArgs e)
        {
            if (Installer.IsGameDirectoryExists(currentGameID) || Directory.Exists(Installer.SourceModsPath() + "alternative_fortresses"))
            {
                string configFilePath = $"./cfg/{currentGameID}/config";
                string args;

                if (File.Exists(configFilePath))
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(configFilePath))
                        {
                            args = sr.ReadLine();
                        }

                        Steam.startAppId(243750, $"-game {Installer.SourceModsPath()}" + "alternative_fortresses" + " " + args);
                    }
                    catch (Exception ex)
                    {
                        new Microsoft.Toolkit.Uwp.Notifications.ToastContentBuilder()
                                .AddText("Aridity")
                                .AddText("There is an error trying to load the game's Aridity configuration. Please try again")
                                .Show();

                        MessageBoxResult msgBoxResult = MessageBox.Show("We are sorry that you are experiencing this problem.\nWe tried to load this game's Aridity config and it fails to read the config.\n" +
                            "You can continue without configurations but the engine currently used in this game can cause issues when changing video settings.\nAre you sure do you want to run this game without" +
                            "configurations?", "Aridity", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                        if(msgBoxResult == MessageBoxResult.Yes)
                        {
                            Steam.startAppId(243750, $"-game {Installer.SourceModsPath()}" + "alternative_fortresses");
                        }
                    }
                }
                else
                {
                    Steam.startAppId(243750, $"-game {Installer.SourceModsPath()}" + "alternative_fortresses");
                }
            }
            else
            {
                this.btnInstallUpdateUninstallPlay.Content = "Downloading";
                this.lblInstallStatus.Content = "Ready!";

                this.btnInstallUpdateUninstallPlay.IsEnabled = false;

                this.installProgress.IsIndeterminate = true;

                this.lblInstallStatus.Visibility = Visibility.Visible;
                this.installProgress.Visibility = Visibility.Visible;

                this.lblInstallStatus.Content = "Downloading...";

                this.installProgress.IsIndeterminate = false;

                this.lblInstallStatus.Content = "Downloading... (no progress)";
                this.installProgress.Value = 50;

                try
                {
                    new Microsoft.Toolkit.Uwp.Notifications.ToastContentBuilder()
                    .AddProgressBar("Downloading Alternative Fortresses...", 1, false, "50", "Downloading...")
                    .AddText("Aridity")
                    .AddText("Aridity is downloading Alternative Fortresses")
                    .Show();
                }
                catch(Exception)
                {
                    // do nothing
                }

                Installer.Install();

                if (Git.error)
                {
                    this.installProgress.Foreground = new SolidColorBrush(Colors.Red);
                    this.lblInstallStatus.Content = "error";
                    MessageBox.Show("There is an error trying to install the game.\nPlease try again later", "Ardity", MessageBoxButton.OK, MessageBoxImage.Error);

                    this.installProgress.Foreground = new SolidColorBrush(Color.FromRgb(6, 216, 243));

                    this.lblInstallStatus.Visibility = Visibility.Hidden;
                    this.installProgress.Visibility = Visibility.Hidden;

                    this.btnInstallUpdateUninstallPlay.Content = "Install";
                }

                this.btnInstallUpdateUninstallPlay.IsEnabled = true;
            }

            if (Installer.IsGameDirectoryExists(currentGameID) || Directory.Exists(Installer.SourceModsPath() + "bf"))
            {
                string configFilePath = $"./cfg/{currentGameID}/config";
                string args;

                if (File.Exists(configFilePath))
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(configFilePath))
                        {
                            args = sr.ReadLine();
                        }

                        Steam.startAppId(243750, $"-game {Installer.SourceModsPath()}" + "bf" + " " + args);
                    }
                    catch (Exception ex)
                    {
                        new Microsoft.Toolkit.Uwp.Notifications.ToastContentBuilder()
                                .AddText("Aridity")
                                .AddText("There is an error trying to load the game's Aridity configuration. Please try again")
                                .Show();

                        MessageBoxResult msgBoxResult = MessageBox.Show("We are sorry that you are experiencing this problem.\nWe tried to load this game's Aridity config and it fails to read the config.\n" +
                            "You can continue without configurations but the engine currently used in this game can cause issues when changing video settings.\nAre you sure do you want to run this game without" +
                            "configurations?", "Aridity", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                        if (msgBoxResult == MessageBoxResult.Yes)
                        {
                            Steam.startAppId(243750, $"-game {Installer.SourceModsPath()}" + "bf");
                        }
                    }
                }
                else
                {
                    Steam.startAppId(243750, $"-game {Installer.SourceModsPath()}" + "bf");
                }
            }
            else
            {
                this.btnInstallUpdateUninstallPlay.Content = "Downloading";
                this.lblInstallStatus.Content = "Ready!";

                this.btnInstallUpdateUninstallPlay.IsEnabled = false;

                this.installProgress.IsIndeterminate = true;

                this.lblInstallStatus.Visibility = Visibility.Visible;
                this.installProgress.Visibility = Visibility.Visible;

                this.lblInstallStatus.Content = "Downloading...";

                this.installProgress.IsIndeterminate = false;

                this.lblInstallStatus.Content = "Downloading... (no progress)";
                this.installProgress.Value = 50;
                try
                {
                    new Microsoft.Toolkit.Uwp.Notifications.ToastContentBuilder()
                    .AddProgressBar("Downloading Beta Fortress...", 1, false, "50", "Downloading...")
                    .AddText("Aridity")
                    .AddText("Aridity is downloading Beta Fortress")
                    .Show();
                }
                catch(Exception)
                {
                    // do nothing
                }

                Installer.Install();

                if (Git.error)
                {
                    this.installProgress.Foreground = new SolidColorBrush(Colors.Red);
                    this.lblInstallStatus.Content = "error";
                    MessageBox.Show("There is an error trying to install the game.\nPlease try again later", "Ardity", MessageBoxButton.OK, MessageBoxImage.Error);

                    this.installProgress.Foreground = new SolidColorBrush(Color.FromRgb(6, 216, 243));

                    this.lblInstallStatus.Visibility = Visibility.Hidden;
                    this.installProgress.Visibility = Visibility.Hidden;

                    this.btnInstallUpdateUninstallPlay.Content = "Install";
                }

                this.btnInstallUpdateUninstallPlay.IsEnabled = true;
            }

            if (Installer.IsGameDirectoryExists(currentGameID) || Directory.Exists(Installer.SourceModsPath() + "pf2"))
            {
                string configFilePath = $"./cfg/{currentGameID}/config";
                string args;

                if (File.Exists(configFilePath))
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(configFilePath))
                        {
                            args = sr.ReadLine();
                        }

                        Steam.startAppId(243750, $"-game {Installer.SourceModsPath()}" + "pf2" + " " + args);
                    }
                    catch (Exception ex)
                    {
                        new Microsoft.Toolkit.Uwp.Notifications.ToastContentBuilder()
                                .AddText("Aridity")
                                .AddText("There is an error trying to load the game's Aridity configuration. Please try again")
                                .Show();

                        MessageBoxResult msgBoxResult = MessageBox.Show("We are sorry that you are experiencing this problem.\nWe tried to load this game's Aridity config and it fails to read the config.\n" +
                            "You can continue without configurations but the engine currently used in this game can cause issues when changing video settings.\nAre you sure do you want to run this game without" +
                            "configurations?", "Aridity", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                        if (msgBoxResult == MessageBoxResult.Yes)
                        {
                            Steam.startAppId(243750, $"-game {Installer.SourceModsPath()}" + "pf2");
                        }
                    }
                }
                else
                {
                    Steam.startAppId(243750, $"-game {Installer.SourceModsPath()}" + "pf2");
                }
            }
            else
            {
                this.btnInstallUpdateUninstallPlay.Content = "Downloading";
                this.lblInstallStatus.Content = "Ready!";

                this.btnInstallUpdateUninstallPlay.IsEnabled = false;
                this.gameList.IsEnabled = false;

                this.installProgress.IsIndeterminate = true;

                this.lblInstallStatus.Visibility = Visibility.Visible;
                this.installProgress.Visibility = Visibility.Visible;

                this.lblInstallStatus.Content = "Downloading...";

                this.installProgress.IsIndeterminate = false;

                this.lblInstallStatus.Content = "Downloading... (no progress)";
                this.installProgress.Value = 50;
                Installer.Install();

                if (Git.error)
                {
                    this.installProgress.Foreground = new SolidColorBrush(Colors.Red);
                    this.lblInstallStatus.Content = "error";
                    MessageBox.Show("There is an error trying to install the game.\nPlease try again later", "Ardity", MessageBoxButton.OK, MessageBoxImage.Error);

                    this.installProgress.Foreground = new SolidColorBrush(Color.FromRgb(6, 216, 243));

                    this.lblInstallStatus.Visibility = Visibility.Hidden;
                    this.installProgress.Visibility = Visibility.Hidden;

                    this.btnInstallUpdateUninstallPlay.Content = "Install";
                }

                this.installProgress.Value = 100;

                this.btnInstallUpdateUninstallPlay.Content = "Play";

                this.btnInstallUpdateUninstallPlay.IsEnabled = true;

                this.gameList.IsEnabled = true;

                this.lblInstallStatus.Visibility = Visibility.Hidden;
                this.installProgress.Visibility = Visibility.Hidden;
            }
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings settingsWindow = new Settings(currentGameID);
            settingsWindow.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            m_notifyIcon.Dispose();
            m_notifyIcon = null;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
                if (m_notifyIcon != null)
                    m_notifyIcon.ShowBalloonTip(2000);
            }
            else
                m_storedWindowState = WindowState;
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CheckTrayIcon();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnShowDevConsole_Click(object sender, RoutedEventArgs e)
        {
            if(!StartupFunctions.IsConsoleAlreadyOpened)
            {
                ConsoleWindow console = new ConsoleWindow();
                console.Show();
            }
            else
            {

            }
        }
    }
}
