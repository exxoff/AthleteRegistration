using AthleteAdmin.UserTypes;
using AthleteAdmin.ViewModels;
using AthleteRegistrationService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace AthleteAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        //protected IMessageText messages;
        public MessageTextManager MessageManager;
        //HostInfo hostInfo;
        public MainWindow()
        {

            //if (MessageManager == null)
            //{
            //    MessageManager = new MessageTextManager();
            //}
            InitializeComponent();

            //hostInfo = new HostInfo();
            //hostInfo.Port = int.Parse(ConfigurationManager.AppSettings["ServerPort"]);
            //hostInfo.DatabaseFile = GetDatabaseFile("LiteDB");
            //if (DatabaseFileExists())
            //{
            //    var response = MessageBox.Show(string.Format("Det finns redan databaser, vill du använda en gammal fil"), "Filer hittade", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            //    if(response == MessageBoxResult.Yes)
            //    {
            //        var f = new Microsoft.Win32.OpenFileDialog();
            //        f.FileName = new FileInfo(hostInfo.DatabaseFile).Name;
            //        f.CheckFileExists = false;
            //        f.Filter = "*.aReg|*.aReg";
            //        f.InitialDirectory = Path.GetTempPath();
            //        if ((bool)f.ShowDialog())
            //        {
            //            hostInfo.DatabaseFile = f.FileName;
            //        }
            //    }
            //}
            //hostInfo.Addresses = GetAllLocalIPv4();

            //this.DataContext = hostInfo;


        }

        //private bool DatabaseFileExists()
        //{

        //    return (Directory.GetFiles(Path.GetTempPath().ToString(),"*.aReg").Length) > 0 ? true : false;

        //}

        ////private List<string> GetLocalIpAddresses()
        ////{
        ////    List<string> _ipList = new List<string>();
        ////    var host = Dns.GetHostEntry(Dns.GetHostName());
        ////    foreach (var ip in host.AddressList)
        ////    {
        ////        if (ip.AddressFamily == AddressFamily.InterNetwork)
        ////        {
        ////            _ipList.Add(ip.ToString());
        ////        }
        ////    }
        ////    return _ipList;
        ////}

        //private List<string> GetAllLocalIPv4()
        //    //private List<string> GetAllLocalIPv4(NetworkInterfaceType _type)
        //{
        //    List<string> ipAddrList = new List<string>();
        //    foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        //    {
        //        //if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
        //            if (item.OperationalStatus == OperationalStatus.Up)
        //        {
        //            foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
        //            {
        //                if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
        //                {
        //                    NetInfo _n = new NetInfo();
        //                    _n.NicType = item.NetworkInterfaceType.ToString();
        //                    _n.IpAddress = ip.Address.ToString();
        //                    _n.Description = item.Name;
        //                    hostInfo.NetworkInfoList.Add(_n);
        //                    //TODO: Remove below
        //                    ipAddrList.Add(ip.Address.ToString());
        //                }
        //            }
        //        }
        //    }
        //    return ipAddrList;
        //}

        //private string GetDatabaseFile(string databaseType)
        //{
        //   return  string.Format("{0}{1}.aReg", Path.GetTempPath(), Guid.NewGuid().ToString());

        //}

        private void StartServer_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void StartServer_Executed(object sender, ExecutedRoutedEventArgs e)
        {


            //ServiceHost host = new ServiceHost(typeof(AthleteRegistrationService.AthleteService));
            //string address = string.Format("net.tcp://localhost:{0}/AthleteRegistration",hostInfo.Port.ToString());
            //Binding binding = new NetTcpBinding();
            //Type contract = typeof(AthleteRegistrationService.IAthleteService);

            //host.AddServiceEndpoint(contract, binding, address);

            //    host.Open();

            //    var hostProxy = new AthleteRegistrationService.AthleteService();
            //    hostProxy.SetDatabaseType("LiteDB");
            //    hostProxy.SetDatabaseFile(hostInfo.DatabaseFile);
            //    hostProxy.StartQueueTimer();
            //if(DataHelper.Messages == null)
            //{
            //    DataHelper.Messages = new System.Collections.ObjectModel.ObservableCollection<string>();
            //}
            //    DataHelper.Messages.Add(string.Format("Servicen startad, lyssnar på port {0}",hostInfo.Port));

        }


        private void Messages_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox txtMessages = sender as ListBox;

            txtMessages.ItemsSource = DataHelper.Messages;
            //txtMessages.ItemsSource = MessageManager.Messages;
        }
    }
}
