using AthleteRegistration.UserTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AthleteRegistration.ViewModels
{
    public class MainViewModel:INotifyPropertyChanged,IDataErrorInfo
    {
        private int bib;
        private string firstName;
        private string lastName;

        private string eMailAddress;
        private bool? isSaved;
        private int athleteId;
        private string saveMessage;

        private Course currentCourse;
        public Course CurrentCourse
        {
            get { return currentCourse; }
            set { currentCourse = value; /*OnPropertyChanged();*/ }
        }


        private bool? isCrew;
        public bool? IsCrew
        {
            get { return isCrew; }
            set
            {
                isCrew = value;
                OnPropertyChanged();
                OnPropertyChanged("Bib");
            }
        }


        public string SaveMessage
        {
            get { return saveMessage; }
            set { saveMessage = value; OnPropertyChanged(); }
        }



        public int Bib
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
            set { firstName = UppercaseFirst(value); OnPropertyChanged(); }
        }



        public string LastName
        {
            get { return lastName; }
            set { lastName = UppercaseFirst(value); OnPropertyChanged(); }
        }


        public string EMailAddress
        {
            get { return eMailAddress; }
            set { eMailAddress = value; OnPropertyChanged(); }
        }

        public bool IsNew { get; set; }



        public MainViewModel()
        {
            IsNew = true;
        }

        private string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        #region Validation members

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }



        private string Validate(string propertyName)
        {
            // Return error message if there is error on else return empty or null string
            string validationMessage = string.Empty;

            if (!IsNew)
            {

                switch (propertyName)
                {
                    case "Bib":
                        {
                            if (Bib == 0 && (IsCrew == null || IsCrew == false))
                            {
                                validationMessage = "Skriv in startnummer eller kryssa i funktionär";


                            }
                            break;
                        }

                    case "FirstName":
                        {

                                if (string.IsNullOrWhiteSpace(FirstName))
                                {
                                    validationMessage = "Fyll i förnamn, tack.";

                                }

                        
                            break;
                        }

                    case "LastName":
                        {

                            if (string.IsNullOrWhiteSpace(LastName))
                            {
                                validationMessage = "Fyll i efternamn, tack.";

                            }


                            break;
                        }

                }
            }

            return validationMessage;
        }

        #endregion

        #region PropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;


        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                if((Bib != 0 || IsCrew == true) && !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName))
                {
                    IsNew = false;
                }
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
