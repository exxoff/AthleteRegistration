using AthleteAdmin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AthleteAdmin.UserTypes
{
    public class MessageTextManager :INotifyPropertyChanged
    {
        //public ObservableCollection<string> Messages { get; set; }

        private ObservableCollection<string> messages;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> Messages
        {
            get { return messages; }
            set { messages = value; OnPropertyChanged(); }
        }


        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public MessageTextManager()
        {
            if(Messages == null)
            {
                Messages = new ObservableCollection<string>();
            }
        }


 

    }
}
