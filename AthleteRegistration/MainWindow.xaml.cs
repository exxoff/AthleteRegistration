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
        private SaveMessage msg;
        
        public MainWindow()
        {


            currentAthlete = new Athlete();
            msg = new SaveMessage();

           InitializeComponent();

            this.DataContext = currentAthlete;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
         
            AthleteService.AthleteDto remoteAthlete  =new AthleteService.AthleteDto()
            {
                Bib = currentAthlete.BIB,
                FirstName = currentAthlete.FirstName,
                LastName = currentAthlete.LastName,
                Course = currentAthlete.Course,
                EMailAddress = currentAthlete.EMailAddress

            };
            
            try
            {
                if (client.StoreAthlete(remoteAthlete))
                {
                    currentAthlete.IsSaved = true;
                    currentAthlete.SaveMessage = string.Format("#{0}, {1} {2}, registrerad för klass {3}.", currentAthlete.BIB, currentAthlete.FirstName, currentAthlete.LastName, currentAthlete.Course);
 
                    
                }
                else
                {
                    currentAthlete.IsSaved = false;
                    currentAthlete.SaveMessage = string.Format("Kunde inte registrera deltagare. Kontrollera med en funktionär.");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(string.Format("{0} \n\n {1}", ex.Message, ex.InnerException.ToString()),"Fel!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
           
        }

        //TODO: Det här funkar inte
        private void AnimationCompleted(object sender, EventArgs e)
        {
            if (currentAthlete.IsSaved != null)
            {
                currentAthlete = new Athlete();
                DataContext = currentAthlete;
            }



        }
    }
}
