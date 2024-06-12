using Microsoft.Win32;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using AlienAbductionDevLauncher.Properties;
using System.Windows.Controls.Primitives;


namespace AlienAbductionDevLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        LaunchConfiguration m_CurrentLaunchConfiguration;

        public MainWindow()
        {
            m_CurrentLaunchConfiguration = new LaunchConfiguration();
            InitializeComponent();
            LoadSettings();
            SetLaunchConfigurationFromSettings();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSettings();
        }

        private void LoadSettings()
        {
            UnrealEditorFilePathTb.Text = Settings.Default.UnrealEnginePath;
            UProjectFilePathTb.Text = Settings.Default.UProjectPath;
            LogCombo.SelectedIndex = Settings.Default.LogMode;
            DisplayModeCombo.SelectedIndex = Settings.Default.DisplayMode;
            PlayerModeCombo.SelectedIndex = Settings.Default.PlayerMode;
        }

        private void SaveSettings()
        {
            Settings.Default.UnrealEnginePath = UnrealEditorFilePathTb.Text;
            Settings.Default.UProjectPath = UProjectFilePathTb.Text;
            Settings.Default.LogMode = (byte)LogCombo.SelectedIndex;
            Settings.Default.DisplayMode = (byte)DisplayModeCombo.SelectedIndex;
            Settings.Default.PlayerMode = (byte)PlayerModeCombo.SelectedIndex;

            Settings.Default.Save();
        }

        private void SetLaunchConfigurationFromSettings()
        {
            m_CurrentLaunchConfiguration.unrealFilePath = Settings.Default.UnrealEnginePath;
            m_CurrentLaunchConfiguration.uProjectFilePath = Settings.Default.UProjectPath;

            // Dummy SelectionChangedEventArgs
            var addedItems = new List<object>();
            var removedItems = new List<object>();
            var selectionChangedEventArgs = new SelectionChangedEventArgs(
                Selector.SelectionChangedEvent,
                removedItems,
                addedItems
                );

            LogCombo_SelectionChanged(LogCombo, selectionChangedEventArgs);
            DisplayModeCmobo_SelectionChanged(DisplayModeCombo, selectionChangedEventArgs);
            PlayerModeCombo_SelectionChanged(PlayerModeCombo, selectionChangedEventArgs);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RunCommand();
        }

        private void RunCommand()
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c START notepad.exe",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (Process process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void BrowseUnrealEditorBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select Unreal Editor .exe";
            openFileDialog.FileName = "*.exe";

            if (openFileDialog.ShowDialog() == true)
            {
                UnrealEditorFilePathTb.Text = openFileDialog.FileName;
                //Settings.Default.UnrealEnginePath = openFileDialog.FileName;
                m_CurrentLaunchConfiguration.unrealFilePath = openFileDialog.FileName;
                Settings.Default.Save();
            }
        }

        private void BrowseUProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select uproject";
            openFileDialog.FileName = "*.uproject";

            if (openFileDialog.ShowDialog() == true)
            {
                UProjectFilePathTb.Text = openFileDialog.FileName;
                //Settings.Default.UProjectPath = openFileDialog.FileName;
                m_CurrentLaunchConfiguration.uProjectFilePath = openFileDialog.FileName;
                Settings.Default.Save();
            }
        }

        private void LaunchBtn_Click(object sender, RoutedEventArgs e)
        {
           


            string launchCmd = string.Empty;
            if (m_CurrentLaunchConfiguration.GetLaunchCommand(ref launchCmd))
            {
                Debug.WriteLine(launchCmd);

                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = "/c " + launchCmd,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    };

                    using (Process process = new Process { StartInfo = startInfo })
                    {
                        process.Start();
                        //process.WaitForExit();

                        //string output = process.StandardOutput.ReadToEnd();
                        //string error = process.StandardError.ReadToEnd();

                        //MessageBox.Show($"Output: {output}\nError: {error}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }



        private void LogCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedLogIndex = LogCombo.SelectedIndex;
            if (selectedLogIndex == 1)
            {
                m_CurrentLaunchConfiguration.logMode = LogMode.log;
            }
            else if (selectedLogIndex == 2)
            {
                m_CurrentLaunchConfiguration.logMode = LogMode.Verbose;
            }
            else
            {
                m_CurrentLaunchConfiguration.logMode = LogMode.None;
            }
        }

        private void DisplayModeCmobo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedDisplayModeIndex = DisplayModeCombo.SelectedIndex;
            if (selectedDisplayModeIndex == 0)
            {
                m_CurrentLaunchConfiguration.displayMode = DisplayMode.Fullscreen;
            }
            else if (selectedDisplayModeIndex == 1)
            {
                m_CurrentLaunchConfiguration.displayMode = DisplayMode.Windowed;
            }
            else if (selectedDisplayModeIndex == 2)
            {
                m_CurrentLaunchConfiguration.displayMode = DisplayMode.BorderlessWindow;
            }
        }

        private void PlayerModeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedPlayerModeIndex = PlayerModeCombo.SelectedIndex;
            if (selectedPlayerModeIndex == 0)
            {
                m_CurrentLaunchConfiguration.playerMode = PlayerMode.VRClient;
            }
            else if (selectedPlayerModeIndex == 1)
            {
                m_CurrentLaunchConfiguration.playerMode = PlayerMode.VRClientFPS;
            }
            else if (selectedPlayerModeIndex == 2)
            {
                m_CurrentLaunchConfiguration.playerMode = PlayerMode.VRCenterHost;
            }
            else if (selectedPlayerModeIndex == 3)
            {
                m_CurrentLaunchConfiguration.playerMode = PlayerMode.ApplicationSet;
            }
        }

    }
}