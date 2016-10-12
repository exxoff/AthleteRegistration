
using AthleteRegistration.Interfaces;
using AthleteRegistration.UserTypes;
using AthleteRegistration.Utils;
using AthleteRegistration.ViewModels;
using AthleteRegistration.Windows;
using AthleteRegistrationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace AthleteRegistration
{

    public partial class MainWindow
    {

        public IAthleteService client;

        public MainWindow()
        {
            InitializeComponent();

            ServiceHelper.ServerAddress = "net.tcp://localhost:9090/AthleteRegistration";
        }

        private void AnimationCompleted(object sender, EventArgs e)
        {

            var vm = (MainViewModel)this.DataContext;
            if (vm.IsSaved)
            {
                vm.ResetViewModel();
               
            }

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


        //TODO: These two methods will be deleted and the function moved to AthleteAdmin 
        private void MenuOpenLotteryWindow_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            LotteryWindow win = new LotteryWindow();
            win.ShowDialog();
        }
        private void MenuOpenLotteryWindow_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
