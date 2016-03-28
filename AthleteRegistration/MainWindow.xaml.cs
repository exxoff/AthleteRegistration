using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using AthleteRegistration.UserTypes;

namespace AthleteRegistration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        
        private Athlete currentAthlete;
        public AthleteService.AthleteServiceClient client = new AthleteService.AthleteServiceClient();
        
        public MainWindow()
        {


            currentAthlete = new Athlete();


           InitializeComponent();

            this.DataContext = currentAthlete;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
         
            currentAthlete.IsSaved = client.StoreAthlete(new AthleteService.AthleteDto()
            {
                Bib = currentAthlete.BIB,
                FirstName = currentAthlete.FirstName,
                LastName = currentAthlete.LastName,
                Course = currentAthlete.Course,
                EMailAddress = currentAthlete.EMailAddress

            });
        }


    }
}
