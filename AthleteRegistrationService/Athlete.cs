using AthleteRegistrationService;
using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AthleteRegistrationService
{
    [DataContract]
    public class Athlete:INotifyPropertyChanged
    {
        private int bib;
        private string firstName;
        private string lastName;
        private string course;
        private string eMailAddress;
        private bool isSaved;
        private int athleteId;

        public event PropertyChangedEventHandler PropertyChanged;

        [DataMember]
        [BsonId]
        public int BIB
        {
            get { return bib; }
            set { bib = value; OnPropertyChanged(); }
        }

        [DataMember]
        public int AthleteId
        {
            get { return athleteId; }
            set { athleteId = value; OnPropertyChanged(); }
        }

        [DataMember]
        public bool IsSaved
        {
            get { return isSaved; }
            set { isSaved = value; OnPropertyChanged(); }
        }


        [DataMember]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value.ToTitleCase(); OnPropertyChanged(); }
        }

        
        [DataMember]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value.ToTitleCase(); OnPropertyChanged(); }
        }

        [DataMember]
        public string Course
        {
            get { return course; }
            set { course = value; OnPropertyChanged(); }
        }

        [DataMember]
        public string EMailAddress
        {
            get { return eMailAddress; }
            set { eMailAddress = value; OnPropertyChanged(); }
        }


        public Athlete()
        {
            IsSaved = false;
        }


        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

 


    }
}
