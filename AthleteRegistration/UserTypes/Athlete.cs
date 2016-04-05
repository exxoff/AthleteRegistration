using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AthleteRegistration.UserTypes
{
    public class Athlete : INotifyPropertyChanged
    {

        private int bib;
        private string firstName;
        private string lastName;
        private string course;
        private string eMailAddress;
        private bool? isSaved;
        private int athleteId;
        private string saveMessage;

        public string SaveMessage
        {
            get { return saveMessage; }
            set { saveMessage = value; OnPropertyChanged(); }
        }



        public int BIB
        {
            get { return bib; }
            set { bib = value; OnPropertyChanged(); }
        }


        public int AthleteId
        {
            get { return athleteId; }
            set { athleteId = value; OnPropertyChanged(); }
        }


        public bool? IsSaved
        {
            get { return isSaved; }
            set { isSaved = value; OnPropertyChanged(); }
        }



        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged(); }
        }



        public string LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged(); }
        }


        public string Course
        {
            get { return course; }
            set { course = value; OnPropertyChanged(); }
        }


        public string EMailAddress
        {
            get { return eMailAddress; }
            set { eMailAddress = value; OnPropertyChanged(); }
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
