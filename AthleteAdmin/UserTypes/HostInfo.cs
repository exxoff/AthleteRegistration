using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AthleteAdmin.UserTypes
{
    public class HostInfo : INotifyPropertyChanged
    {
        
        private int port;
        public int Port
        {
            get { return port; }
            set { port = value; OnPropertyChanged(); }
        }

        private string databaseFile;
        public string DatabaseFile
        {
            get { return databaseFile; }
            set { databaseFile = value; OnPropertyChanged(); }
        }

        private List<string> adresses;
        public List<string> Addresses
        {
            get { return adresses; }
            set { adresses = value; OnPropertyChanged(); }
        }

        private List<Netinfo> networkInfoList;
        public List<Netinfo> NetworkInfoList
        {
            get { return networkInfoList; }
            set { networkInfoList = value; OnPropertyChanged(); }
        }


        public HostInfo()
        {
            if(Addresses == null)
            {
                Addresses = new List<string>();
            }

            if(NetworkInfoList == null)
            {
                NetworkInfoList = new List<Netinfo>();
            }
        }

        #region PropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
