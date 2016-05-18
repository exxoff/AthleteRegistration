using AthleteRegistration.UserTypes;
using AthleteRegistration.ViewModels;
using AthleteRegistration.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AthleteRegistration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        
        private MainViewModel viewModel;
        public AthleteService.AthleteServiceClient client = new AthleteService.AthleteServiceClient();
        private SaveMessage msg;
        private List<Course> listOfCourses;

        public MainWindow()
        {


            viewModel = new MainViewModel()
            {
                CurrentCourse = new Course()
                {
                    Wave = "1",
                    Caption = "LÅNG",
                    Group = "Long"
                },
                IsCrew = false            
            };
            
            msg = new SaveMessage();


           InitializeComponent();

            System.Timers.Timer CheckServiceTimer = new System.Timers.Timer(3000);
            CheckServiceTimer.Elapsed += CheckServiceTimer_Elapsed;
            CheckServiceTimer.Enabled = true;

            this.DataContext = viewModel;
            txtBib.Focus();
        }

        private void CheckServiceTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                viewModel.IsAlive = client.IsAlive();
            }
            catch (Exception)
            {

                viewModel.IsAlive = false;
            }
            
        }

        private void AnimationCompleted(object sender, EventArgs e)
        {
            if (viewModel.IsSaved != null)
            {
                viewModel = new MainViewModel();
                DataContext = viewModel;
                cboCourses.SelectedIndex = 0;
                txtBib.Focus();
            }



        }

        private void Crew_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox _box = sender as CheckBox;
            viewModel.IsCrew = (bool)_box.IsChecked ? true : false;

        }

        private void Course_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton _radio = sender as RadioButton;

            if(_radio.Tag != null)
            {
                string[] _courseInfo = _radio.Tag.ToString().Split(',');

                viewModel.CurrentCourse = new Course()
                {
                    Wave = _courseInfo[0],
                    Caption = _courseInfo[1],
                    Group = _courseInfo[2]
                };
            }
            


            
        }

        private void Courses_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            if(listOfCourses == null)
            {
                listOfCourses = new List<Course>();
                listOfCourses.Add(new Course() { Caption = "LÅNG", Wave = "1", Group = "Long" });
                listOfCourses.Add(new Course() { Caption = "KORT", Wave = "2", Group = "Short" });


            }
            box.ItemsSource = listOfCourses;
        }

        private void MenuOpenStartHostWindow_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            StartServiceWindow win = new StartServiceWindow();
            win.ShowDialog();
        }

        private void MenuOpenStartHostWindow_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = viewModel.IsAlive ? false : true;
        }

        private void MenuOpenCsvExportWindow_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ExportCsvWindow win = new ExportCsvWindow();
            win.ShowDialog();
        }

        private void MenuOpenLotteryWindow_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            LotteryWindow win = new LotteryWindow();
            win.ShowDialog();
        }

        private void SubmitAthlete_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {

            if (viewModel.IsCrew == true)
            {
                viewModel.Bib = -1;
            }

            AthleteService.AthleteDto remoteAthlete = new AthleteService.AthleteDto()
            {
                Bib = viewModel.Bib,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Group = viewModel.CurrentCourse.Group,
                WaveNumber = viewModel.CurrentCourse.Wave,
                EMailAddress = viewModel.EMailAddress

            };
            try
            {

                AthleteService.AthleteDto _existingAthlete = client.ExistingAthlete(viewModel.Bib);
                if (_existingAthlete != null)
                {
                    if (MessageBox.Show(string.Format("Nummer {0} har redan registrerats av {1} {2}. Vill du uppdatera?", _existingAthlete.Bib, _existingAthlete.FirstName, _existingAthlete.LastName),
                        "Dublett", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                if (client.StoreAthlete(remoteAthlete))
                {
                    viewModel.IsSaved = true;
                    if (viewModel.IsCrew == true)
                    {
                        viewModel.SaveMessage = string.Format("Funktionär {0} {1} registrerad. Tack för din hjälp.", viewModel.FirstName, viewModel.LastName);
                    }
                    else
                    {
                        viewModel.SaveMessage = string.Format("#{0}, {1} {2}, registrerad för klass {3}.", viewModel.Bib, viewModel.FirstName, viewModel.LastName, viewModel.CurrentCourse.Caption);
                    }



                }
                else
                {
                    viewModel.IsSaved = false;
                    viewModel.SaveMessage = string.Format("Kunde inte registrera deltagare. Kontrollera med en funktionär.");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(string.Format("{0} \n\n {1}", ex.Message, ex.InnerException == null ? null : ex.InnerException.ToString()), "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void SubmitAthlete_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (viewModel.IsNew)
            {
                e.CanExecute = false;
                return;
            }

                e.CanExecute = IsValid(sender as DependencyObject);

            
        }

        private bool IsValid(DependencyObject obj)
        {
            // The dependency object is valid if it has no errors and all
            // of its children (that are dependency objects) are error-free.
            return !Validation.GetHasError(obj) &&
            LogicalTreeHelper.GetChildren(obj)
            .OfType<DependencyObject>()
            .All(IsValid);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void MenuOpenCsvExportWindow_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = viewModel.IsAlive;
        }

        private void MenuOpenLotteryWindow_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = viewModel.IsAlive;
        }
    }
}
