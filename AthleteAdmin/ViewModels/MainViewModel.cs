using AthleteAdmin.Interfaces;
using AthleteAdmin.UserTypes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace AthleteAdmin.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public IHostInfo hostInfo { get; set; }
        /// <summary>
        /// Initializes a new instance of the MvvmViewModel1 class.
        /// </summary>
        public MainViewModel(IHostInfo hostInfo,IDialogService dialogService)
        {
            this.hostInfo = hostInfo;
            hostInfo.Port = int.Parse(ConfigurationManager.AppSettings["ServerPort"]);
            hostInfo.DatabaseFile = GetDatabaseFile("LiteDB");
            //hostInfo.Addresses = 
            GetAllLocalIPv4();
        }

        private bool DatabaseFileExists()
        {

            return (Directory.GetFiles(Path.GetTempPath().ToString(), "*.aReg").Length) > 0 ? true : false;

        }

        private void GetAllLocalIPv4()
        
        {
            //List<string> ipAddrList = new List<string>();
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                
                if (item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            INetInfo _n = new NetInfo();
                            _n.NicType = item.NetworkInterfaceType.ToString();
                            _n.IpAddress = ip.Address.ToString();
                            _n.Description = item.Name;
                            hostInfo.NetworkInfoList.Add(_n);
                            //TODO: Remove below
                            //ipAddrList.Add(ip.Address.ToString());
                        }
                    }
                }
            }
            //return ipAddrList;
        }

        private string GetDatabaseFile(string databaseType)
        {
            return string.Format("{0}{1}.aReg", Path.GetTempPath(), Guid.NewGuid().ToString());

        }

        private void StartService()
        {
            ServiceHost host = new ServiceHost(typeof(AthleteRegistrationService.AthleteService));
            string address = string.Format("net.tcp://localhost:{0}/AthleteRegistration", hostInfo.Port.ToString());
            Binding binding = new NetTcpBinding();
            Type contract = typeof(AthleteRegistrationService.IAthleteService);

            host.AddServiceEndpoint(contract, binding, address);

            host.Open();

            var hostProxy = new AthleteRegistrationService.AthleteService();
            hostProxy.SetDatabaseType("LiteDB");
            hostProxy.SetDatabaseFile(hostInfo.DatabaseFile);
            hostProxy.StartQueueTimer();
        }
    }
}