using AthleteRegistration.Interfaces;
using AthleteRegistration.UserTypes;
using AthleteRegistration.Utils;
using AthleteRegistrationService;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AthleteRegistration.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged, IDataErrorInfo, IMainViewModel
    {

        #region Athlete Properties
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
        private int bib;
        public int Bib
        {
            get { return bib; }
            set { bib = value; OnPropertyChanged(); }
        }
        private int athleteId;
        public int AthleteId
        {
            get { return athleteId; }
            set { athleteId = value; OnPropertyChanged(); }
        }
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = UppercaseFirst(value);
                OnPropertyChanged();
                SubmitAthlete.RaiseCanExecuteChanged();
            }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = UppercaseFirst(value); OnPropertyChanged(); }
        }
        private string eMailAddress;
        public string EMailAddress
        {
            get { return eMailAddress; }
            set { eMailAddress = value; OnPropertyChanged(); }
        }
        #endregion

        #region UI Properties
        public bool IsNew { get; set; }
        private bool isAlive;
        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; OnPropertyChanged(); }
        }
        public bool HideEmailField { get; set; } = false;
        private List<Course> courses;
        public List<Course> Courses
        {
            get { return courses; }
            set { courses = value; }
        }
        public RelayCommand SubmitAthlete { get; set; }
        public RelayCommand AnimationCompleted { get; set; }
        private string saveMessage;
        public string SaveMessage
        {
            get { return saveMessage; }
            set { saveMessage = value; OnPropertyChanged(); }
        }
        private bool isSaved = false;
        public bool IsSaved
        {
            get { return isSaved; }
            set { isSaved = value; OnPropertyChanged(); }
        }
        private IAthlete athlete;
        private bool DoValidation;
        private bool isBibFocused;
        public bool IsBibFocused
        {
            get { return isBibFocused; }
            set { isBibFocused = value; OnPropertyChanged(); }
        }
        private int selectedCourseIndex;
        public int SelectedCourseIndex
        {
            get { return selectedCourseIndex; }
            set { selectedCourseIndex = value; OnPropertyChanged(); }
        }
        public bool EnableEmailField { get; private set; }
        #endregion

        [PreferredConstructor]
        public MainViewModel() : this(new Athlete()) { }

        
        public MainViewModel(IAthlete athlete)
        {
            SubmitAthlete = new RelayCommand(() => SendAthleteToService(), () => OkToSendAthlete());
            AnimationCompleted = new RelayCommand(() => ResetViewModel());


            this.athlete = athlete;


            System.Timers.Timer CheckServiceTimer = new System.Timers.Timer(1000);
            CheckServiceTimer.Elapsed += CheckServiceTimer_Elapsed;
            CheckServiceTimer.Enabled = true;


            ResetViewModel();


            LoadCourses();
            GetEmailVisibility();
            SaveMessage = string.Empty;


        }

        private void CheckServiceTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                string serviceAddress = ServiceHelper.ServerAddress;
                var binding = new NetTcpBinding();

                var channelFactory = new ChannelFactory<IAthleteService>(binding, serviceAddress);

                var client = channelFactory.CreateChannel();
                IsAlive = client.IsAlive();

            }
            catch (Exception)
            {

                IsAlive = false;
            }

        }

        private bool OkToSendAthlete()
        {

            //TODO: return true/false
            return true;
        }

        private void SendAthleteToService()
        {

            if (this.IsCrew == true)
            {
                this.Bib = -1;
            }

            AthleteDto remoteAthlete = new AthleteDto()

            {
                Bib = this.Bib,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Group = this.CurrentCourse.Group,
                WaveNumber = this.CurrentCourse.Wave,
                EMailAddress = this.EMailAddress

            };
            try
            {
                var channelFactory = ServiceHelper.GetFactory();
                var serviceClient = channelFactory.CreateChannel();
                AthleteDto _existingAthlete = serviceClient.ExistingAthlete(this.Bib);
                if (_existingAthlete != null)
                {
                    if (MessageBox.Show(string.Format("Nummer {0} har redan registrerats av {1} {2}. Vill du uppdatera?", _existingAthlete.Bib, _existingAthlete.FirstName, _existingAthlete.LastName),
                        "Dublett", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                if (serviceClient.StoreAthlete(remoteAthlete))
                {
                    this.IsSaved = true;
                    if (this.IsCrew == true)
                    {
                        this.SaveMessage = string.Format("Funktionär {0} {1} registrerad. Tack för din hjälp.", this.FirstName, this.LastName);
                    }
                    else
                    {
                        this.SaveMessage = string.Format("#{0}, {1} {2}, registrerad för klass {3}.", this.Bib, this.FirstName, this.LastName, this.CurrentCourse.Caption);
                    }

                    

                }
                else
                {
                    this.IsSaved = false;
                    this.SaveMessage = string.Format("Kunde inte registrera deltagare. Kontrollera med en funktionär.");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(string.Format("{0} \n\n {1}", ex.Message, ex.InnerException == null ? null : ex.InnerException.ToString()), "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void GetEmailVisibility()
        {
            try
            {
                string _tempSetting = ConfigurationManager.AppSettings["EnableEmailField"];

                if (_tempSetting == "1")
                {
                    //HideEmailField = true;
                    EnableEmailField = true;
                }
            }
            catch (ArgumentException)
            {

            }
        }

        private void LoadCourses()
        {
            if (Courses == null)
            {
                Courses = new List<Course>();
                Courses.Add(new Course() { Caption = "LÅNG", Wave = "1", Group = "Long" });
                Courses.Add(new Course() { Caption = "KORT", Wave = "2", Group = "Short" });
            }
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


        public void ResetViewModel()
        {

            DoValidation = false;
            IsBibFocused = false;
            IsNew = true;
            IsCrew = false;
            IsAlive = true;
            IsSaved = false;
            SelectedCourseIndex = 0;
            Bib = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
            EMailAddress = string.Empty;


            SaveMessage = string.Empty;

            
            DoValidation = true;

            IsBibFocused = true;

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
                if(!DoValidation)
                {
                    return null;
                }

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
                if ((Bib != 0 || IsCrew == true) && !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName))
                {
                    IsNew = false;
                }

                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
