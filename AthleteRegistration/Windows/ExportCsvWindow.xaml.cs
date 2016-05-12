using AthleteRegistration.ViewModels;
using System;
using System.Collections.Generic;
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
using Microsoft.WindowsAPICodePack.Dialogs;
using AthleteRegistration.Utils;

namespace AthleteRegistration.Windows
{
    /// <summary>
    /// Interaction logic for ExportCsvWindow.xaml
    /// </summary>
    public partial class ExportCsvWindow
    {
        ExportCsvViewModel viewModel;
        public ExportCsvWindow()
        {
            viewModel = new ExportCsvViewModel();
            InitializeComponent();
            DataContext = viewModel;
        }

        private void OpenSaveDiaolog_Executed(object sender, ExecutedRoutedEventArgs e)
        {


            var f = new Microsoft.Win32.SaveFileDialog();
            f.Filter = "*.csv|*.csv";
            f.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if((bool)f.ShowDialog())
            {
                viewModel.ExportFile = new System.IO.FileInfo(f.FileName);
            }

            

        }

        private void SaveCsv_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                CsvHelper.GenerateCsv(viewModel.ExportFile, viewModel.Athletes);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            this.Close();
        }
    }
}
