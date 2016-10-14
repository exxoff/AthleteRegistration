using AthleteAdmin.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AthleteAdmin.UserTypes
{
    public class DialogService : IDialogService
    {
        public string FileOpenDialog()
        {
            string DefaultPath = string.Format("{0}{1}.aReg", Path.GetTempPath(), Guid.NewGuid().ToString());
            var f = new Microsoft.Win32.OpenFileDialog();
            f.FileName = new FileInfo(DefaultPath).Name;
            f.CheckFileExists = false;
            f.Filter = "*.aReg|*.aReg";
            f.InitialDirectory = Path.GetTempPath();
            if ((bool)f.ShowDialog())
            {
                return f.FileName;
            }
            return DefaultPath; ;

        }

        public bool DisplayYesNoMessageBoxDialog(string Message,string Title,bool showCancelButton = false)
        {

            var result = MessageBox.Show(Message, Title, 
                showCancelButton ? MessageBoxButton.YesNoCancel : MessageBoxButton.YesNo);

            if(result == MessageBoxResult.Yes)
            {
                return true;
            }
            return false;
        }
    }
}
