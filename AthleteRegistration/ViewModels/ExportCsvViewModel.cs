using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AthleteRegistration.AthleteService;
using System.IO;

namespace AthleteRegistration.ViewModels
{
    public class ExportCsvViewModel : INotifyPropertyChanged
    {

        private List<AthleteDto> athletes;
        public List<AthleteDto> Athletes
        {
            get { return athletes; }
            set { athletes = value; OnPropertyChanged(); }
        }

        private FileInfo exportFile;
        public FileInfo ExportFile
        {
            get { return exportFile; }
            set { exportFile = value; OnPropertyChanged(); }
        }

        private int numberOfAthletes;
        public int NumberOfAthletes
        {
            get { return numberOfAthletes; }
            set { numberOfAthletes = value; OnPropertyChanged(); }
        }


        public ExportCsvViewModel()
        {
            //Athletes = new List<AthleteDto>();

            try
            {
                Athletes = new AthleteServiceClient().GetAllAthletes();
                NumberOfAthletes = Athletes.Count();
            }
            catch (Exception)
            {

                throw;
            }
            

            }



        #region PropertyChangedMemebers
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
