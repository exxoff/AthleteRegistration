using AthleteRegistration.UserTypes;
using AthleteRegistration.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace AthleteRegistration.Windows
{
    /// <summary>
    /// Interaction logic for LotteryWindow.xaml
    /// </summary>
    public partial class LotteryWindow
    {
        private LotteryViewModel model;
        public LotteryWindow()
        {
            model = new LotteryViewModel() { NumberOfWinners=10,
                                            Winners=new ObservableCollection<Athlete>()};

            InitializeComponent();

            DataContext = model;
        }

        private void CreateList_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(model.Athletes.Count < 1)
            {
                e.CanExecute = false;
                return;
            }

            if(model.NumberOfWinners < 1)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = true;
        }

        private void CreateList_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int maxIndex = model.Athletes.Count - 1;

            List<Athlete> tempList = model.Athletes;
            Random rnd = new Random();
            
            for (int i = 0; i < model.NumberOfWinners ; i++)
            {
                if (tempList.Count() == 0)
                {
                    return;
                }
                var randomIndex = rnd.Next(tempList.Count());
                model.Winners.Add(tempList[randomIndex]);
                tempList.RemoveAt(randomIndex);
            }
        }
    }
}
