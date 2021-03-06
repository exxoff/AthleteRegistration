﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AthleteRegistration.UserTypes
{
    public class HostInformation : INotifyPropertyChanged
    {

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(); }
        }

        private string databaseType;
        public string DatabaseType
        {
            get { return databaseType; }
            set { databaseType = value; OnPropertyChanged(); }
        }

        private string databaseFile;
        public string DatabaseFile
        {
            get { return databaseFile; }
            set { databaseFile = value; OnPropertyChanged(); }
        }
        private BitmapSource shieldIcon;
        public BitmapSource ShieldIcon
        {
            get { return shieldIcon; }
            set { shieldIcon = value; OnPropertyChanged(); }
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
