using AthleteRegistration.UserTypes;
using AthleteRegistration.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

namespace AthleteRegistration.Windows
{
    /// <summary>
    /// Interaction logic for StartServiceWindow.xaml
    /// </summary>
    public partial class StartServiceWindow
    {
        private static HostInformation hostInfo;
        private ObservableCollection<string> dbTypes;
        public StartServiceWindow()
        {
            dbTypes = new ObservableCollection<string>() { "LiteDB" };
            
            hostInfo = new HostInformation();
            hostInfo.Address = ConfigurationManager.AppSettings["DefaultAddress"];
            hostInfo.DatabaseType = ConfigurationManager.AppSettings["DefaultDatabaseType"];
            hostInfo.DatabaseFile = GetDatabaseFile(hostInfo.DatabaseType);
            hostInfo.ShieldIcon = IconResource.GetShieldIcon();
            InitializeComponent();
            cboDbType.ItemsSource = dbTypes;
            this.DataContext = hostInfo;
            cboDbType.SelectedIndex = 0;

        }

        private string GetDatabaseFile(string databaseType)
        {
            string file = string.Format("{0}\\AthleteRegistration_{1}.tmp", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DateTime.Now.ToString("yyyyMMdd"));
            

            switch (databaseType)
            {
                case ("LiteDB"):
                    

                    return Path.ChangeExtension(file, ".LiteDB");

                case ("SQLite"):
                    
                    return Path.ChangeExtension(file, ".SQLite");
                default:
                    return null; ;
            }
        }

        private void StartHost_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void StartHost_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProcessStartInfo pInfo = new ProcessStartInfo();
            try
            { 
            
                pInfo.WorkingDirectory = ConfigurationManager.AppSettings["ServerDirectory"];
                pInfo.FileName = string.Format("{0}\\{1}",pInfo.WorkingDirectory,"AthleteRegistrationHost.exe");
                pInfo.Arguments = string.Format("{0} {1} {2}", hostInfo.Address, hostInfo.DatabaseType, hostInfo.DatabaseFile);
                pInfo.UseShellExecute = true;
                pInfo.Verb = "runas";
                Process p = new Process();
                p.StartInfo = pInfo;
                p.Start();
                this.Close();
            }
            catch(FileNotFoundException)
            {
                MessageBox.Show(string.Format("Kunde inte hitta {0}.", pInfo.FileName),"Fel!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show(string.Format("{0}", ex.Message), "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cboDbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hostInfo.DatabaseFile = GetDatabaseFile(cboDbType.SelectedItem.ToString());
        }

        private void OpenSaveDiaolog_Executed(object sender, ExecutedRoutedEventArgs e)
        {


            var f = new Microsoft.Win32.OpenFileDialog();
            f.FileName = new FileInfo(hostInfo.DatabaseFile).Name;
            f.CheckFileExists = false;
            f.Filter = "*.LiteDB|*.LiteDB";
            f.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if ((bool)f.ShowDialog())
            {
                hostInfo.DatabaseFile = f.FileName;
            }

        }
    }
}
