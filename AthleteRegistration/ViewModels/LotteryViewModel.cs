using AthleteRegistration.AthleteService;
using AthleteRegistration.UserTypes;
using AthleteRegistration.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AthleteRegistration.ViewModels
{
    class LotteryViewModel : INotifyPropertyChanged
    {


        private List<Athlete> athletes;
        public List<Athlete> Athletes
        {
            get { return athletes; }
            set { athletes = value; OnPropertyChanged(); }
        }


        private ObservableCollection<Athlete> winners;
        public ObservableCollection<Athlete> Winners
        {
            get { return winners; }
            set { winners = value; OnPropertyChanged(); }
        }

        private int numberOfWinners;
        public int NumberOfWinners
        {
            get { return numberOfWinners; }
            set { numberOfWinners = value; OnPropertyChanged(); }
        }




        public LotteryViewModel()
        {
            try
            {
                Athletes = new AthleteServiceClient().GetAllAthletes(true).ToAthleteCollection();
                
            }
            catch (Exception)
            {

                throw;
            }


        }
    


        #region PropertyChanged Members
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
