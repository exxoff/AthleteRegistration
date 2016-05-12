﻿using AthleteRegistration.UserTypes;
using AthleteRegistration.ViewModels;
using AthleteRegistration.Windows;
using System;
using System.Collections.Generic;
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
                    Caption = "Lång",
                    Group = "Long"
                },
                IsCrew = false            
            };
            
            msg = new SaveMessage();

           InitializeComponent();

            this.DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
         
            if (viewModel.IsCrew == true)
            {
                viewModel.BIB = -1;
            }

            AthleteService.AthleteDto remoteAthlete  =new AthleteService.AthleteDto()
            {
                Bib = viewModel.BIB,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Group = viewModel.CurrentCourse.Group,
                WaveNumber=viewModel.CurrentCourse.Wave,
                EMailAddress = viewModel.EMailAddress

            };
            try
            {

                AthleteService.AthleteDto _existingAthlete = client.ExistingAthlete(viewModel.BIB);
                if(_existingAthlete != null)
                {
                    if(MessageBox.Show(string.Format("Nummer {0} har redan registrerats av {1} {2}. Vill du uppdatera?",_existingAthlete.Bib, _existingAthlete.FirstName, _existingAthlete.LastName),
                        "Dublett",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                if (client.StoreAthlete(remoteAthlete))
                {
                    viewModel.IsSaved = true;
                    if(viewModel.IsCrew == true)
                    {
                        viewModel.SaveMessage = string.Format("Funktionär {0} {1} registrerad. Tack för din hjälp.", viewModel.FirstName, viewModel.LastName);
                    }
                    else
                    {
                        viewModel.SaveMessage = string.Format("#{0}, {1} {2}, registrerad för klass {3}.", viewModel.BIB, viewModel.FirstName, viewModel.LastName, viewModel.CurrentCourse.Caption);
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

                MessageBox.Show(string.Format("{0} \n\n {1}", ex.Message, ex.InnerException == null ? null : ex.InnerException.ToString()),"Fel!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
           
        }

       
        private void AnimationCompleted(object sender, EventArgs e)
        {
            if (viewModel.IsSaved != null)
            {
                viewModel = new MainViewModel();
                DataContext = viewModel;
                cboCourses.SelectedIndex = 0;
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
                listOfCourses.Add(new Course() { Caption = "Lång", Wave = "1", Group = "Long" });
                listOfCourses.Add(new Course() { Caption = "Kort", Wave = "2", Group = "Short" });


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
            e.CanExecute = true;
        }

        private void MenuOpenCsvExportWindow_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ExportCsvWindow win = new ExportCsvWindow();
            win.ShowDialog();
        }
    }
}
