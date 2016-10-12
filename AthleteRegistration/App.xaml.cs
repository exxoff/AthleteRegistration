using AthleteRegistration.Interfaces;
using AthleteRegistration.UserTypes;
using AthleteRegistration.ViewModels;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AthleteRegistration
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ComposeObjects();
            Application.Current.MainWindow.Show();
        }

        private void ComposeObjects()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IAthlete,Athlete>();
            //SimpleIoc.Default.Register<ICsvFileWriter, CsvFileWriter>();


            //MainViewModel viewModel = new MainViewModel(ServiceLocator.Current.GetInstance<IAthlete>());
            //MainWindow = new MainWindow(viewModel);

            MainWindow = new MainWindow();

        }
    }
}
