using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AthleteRegistration.UserTypes
{
    public class SaveMessage : INotifyPropertyChanged
    {
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(); }
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
