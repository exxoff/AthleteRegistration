using AthleteAdmin.Interfaces;
using AthleteAdmin.UserTypes;
using AthleteMessageService.Interfaces;
using AthleteMessageService.UserTypes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace AthleteAdmin.ViewModels
{



    public class MainViewModel : INotifyPropertyChanged
    {

        #region Properties and variables
        private int port;
        public int Port
        {
            get { return port; }
            set
            {
                port = value;
                OnPropertyChanged();
                hostInfo.Port = port;
                StartServiceCommand.RaiseCanExecuteChanged();
            }
        }

        private string databaseFile;
        public string DatabaseFile
        {
            get { return databaseFile; }
            set
            {
                databaseFile = value;
                OnPropertyChanged();
                hostInfo.DatabaseFile = databaseFile;
                StartServiceCommand.RaiseCanExecuteChanged();
            }
        }

        public IMessageRepository messageRepository { get; set; }

        public IHostInfo hostInfo { get; set; }

        private IDialogService dialogService;

        public RelayCommand StartServiceCommand { get; set; }
        public IDialogService DialogService
        {
            get
            {
                if (dialogService == null)
                {
                    dialogService = new DialogService();
                }
                return dialogService;
            }
            set { dialogService = value; }
        }


        private ObservableCollection<String> messages;
        public ObservableCollection<String> Messages
        {
            get
            {
                if (messages == null)
                {
                    messages = new ObservableCollection<string>();
                }
                return messages;
            }
            set
            {
                messages = value;
                OnPropertyChanged();
            }
        }

        private bool isStartButton;
        public bool IsStartButton
        {
            get { return isStartButton; }
            set { isStartButton = value; OnPropertyChanged(); }
        }


        private ServiceHost host;
        #endregion


        public MainViewModel(IHostInfo hostInfo, IDialogService dialogService)
        {
            if (messageRepository == null)
            {
                this.messageRepository = MessageRepository.Instance;
            }
            //this.messageRepository = messageRepository;
            StartServiceCommand = new RelayCommand(() => StartService(), () => HostInfoIsValid());
            this.DialogService = dialogService;
            this.hostInfo = hostInfo;

            int _portNumber;
            if (int.TryParse(ConfigurationManager.AppSettings["ServerPort"], out _portNumber))
            {
                Port = _portNumber;
            }
            hostInfo.DatabaseFile = GetDatabaseFile();
            GetAllLocalIPv4();
            StartMessageRepository();
            IsStartButton = true;
        }

        private bool HostInfoIsValid()
        {
            if (hostInfo.Port > 0 && hostInfo.Port <= Int16.MaxValue)
            {
                return true;
            }
            return false;
        }

        private bool UseOldFile(string Message, string Title)
        {
            return DialogService.DisplayYesNoMessageBoxDialog(Message, Title, false);
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
                        }
                    }
                }
            }
        }

        private string GetDatabaseFile()
        {

            if (DatabaseFileExists())
            {
                // Fråga om man vill använda en gammal fil
                if (UseOldFile("Files exist, do you want to use an old file?", "File exist"))
                {
                    // Om ja
                    return hostInfo.DatabaseFile = DialogService.FileOpenDialog();
                }

                //Om nej/Cancel
            }
            return string.Format("{0}{1}.aReg", Path.GetTempPath(), Guid.NewGuid().ToString());

        }

        private void StartService()
        {

            if (IsStartButton)
            {



                host = new ServiceHost(typeof(AthleteRegistrationService.AthleteService));
                string address = string.Format("net.tcp://localhost:{0}/AthleteRegistration", hostInfo.Port.ToString());
                Binding binding = new NetTcpBinding();
                Type contract = typeof(AthleteRegistrationService.IAthleteService);

                host.AddServiceEndpoint(contract, binding, address);

                host.Open();

                IsStartButton = false;
                messageRepository.ReceiveMessage(string.Format("Server startad, lyssnar på port {0}", hostInfo.Port));
                var hostProxy = new AthleteRegistrationService.AthleteService();
                hostProxy.SetDatabaseType("LiteDB");
                hostProxy.SetDatabaseFile(hostInfo.DatabaseFile);
                hostProxy.StartQueueTimer();
            }
            else
            {
                host.Close();
                messageRepository.ReceiveMessage("Server stoppad.");
                IsStartButton = true;
            }
        }



        private void StartMessageRepository()
        {
            ServiceHost host = new ServiceHost(typeof(MessageRepository));
            string address = "net.pipe://localhost/AthleteRegMessage";
            Binding binding = new NetNamedPipeBinding();
            binding.Name = "MessageReceiver";
            Type contract = typeof(IMessageRepository);

            host.AddServiceEndpoint(contract, binding, address);

            host.Open();

            messageRepository.ReceiveMessage("Meddelandeservicen startad.");


        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}