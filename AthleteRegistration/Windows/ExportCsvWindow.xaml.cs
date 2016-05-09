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

namespace AthleteRegistration.Windows
{
    /// <summary>
    /// Interaction logic for ExportCsvWindow.xaml
    /// </summary>
    public partial class ExportCsvWindow
    {
        public ExportCsvWindow()
        {
            ExportCsvViewModel viewModel = new ExportCsvViewModel();
            InitializeComponent();
            DataContext = viewModel;
        }

        private void OpenSaveDiaolog_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //var d = new CommonSaveFileDialog();
            //d.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //d.EnsurePathExists = true;
            //d.Filters = "*.csv";
            //d.DefaultExtension= ".csv";
            //d.ShowDialog();

            var f = new Microsoft.Win32.SaveFileDialog();
            f.Filter = "*.csv|*.csv";
            f.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            f.ShowDialog();

        }
    }
}
