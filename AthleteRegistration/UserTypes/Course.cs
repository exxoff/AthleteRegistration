using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AthleteRegistration.UserTypes
{
    public class Course : INotifyPropertyChanged
    {

        private string caption;
        public string Caption
        {
            get { return caption; }
            set { caption = value; OnPropertyChanged(); }
        }

        private string wave;
        public string Wave
        {
            get { return wave; }
            set { wave = value; OnPropertyChanged(); }
        }

        private string group;
        public string Group
        {
            get { return group; }
            set { group = value; OnPropertyChanged(); }
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
